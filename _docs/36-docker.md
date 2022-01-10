# Docker

# tldr;

docker pull mcr.microsoft.com/dotnet/sdk:6.0

docker build -t testapp .

## Containers

List containers

`docker ps -a`

Run containers

docker run -d tmp-ubuntu

docker run -it --rm -p 5200:80 --name pizzabackendcontainer pizzabackend

docker run -it --rm -p 5200:80 --name pizzabackendcontainer <image-name>

docker run -dp 3000:3000 getting-started


docker run -it --name testalpinegit alpine/git


Params
-d              - run the container in detached mode (in the background)
-p 8080:80      - map port 8080 of the host to port 80 in the container
pizzabackend    - the image to use

--rm 		Automatically remove the container when it exits
--interactive , -i 		Keep STDIN open even if not attached
--tty , -t 		        Allocate a pseudo-TTY


Stop

docker stop <the-container-id>
docker rm -f <the-container-id>


## Images

List images

`docker images`

docker pull [OPTIONS] NAME[:TAG|@DIGEST]

docker scan getting-started
docker image history getting-started


docker build -t pizzabackend .

-t flag tags our image




mcr.microsoft.com/dotnet/aspnet:6.0 	ASP.NET Core, with runtime only and ASP.NET Core optimizations, on Linux and Windows (multi-arch)
mcr.microsoft.com/dotnet/sdk:6.0 	.NET 6, with SDKs included, on Linux and Windows (multi-arch)

dotnet/sdk          : .NET SDK
dotnet/aspnet       : ASP.NET Core Runtime
dotnet/runtime      : .NET Runtime
dotnet/runtime-deps : .NET Runtime Dependencies
dotnet/monitor      : .NET Monitor Tool
dotnet/samples      : .NET Samples


## Volumes

docker volume ls

Named volumes (volume is maintained by Docker; good for data persistence without need to share data with host) 
docker volume create todo-db

                            <named>:<container path>
docker run -dp 3000:3000 -v todo-db:/etc/todos getting-started

docker volume inspect todo-db

-v flag to specify a volume mount. 
We will use the named volume and mount it to /etc/todos, which will capture all files created at the path.

Bind mounts

docker run -dp 3000:3000 -w /app -v "$(pwd):/app"

    node:12-alpine `
    sh -c "yarn install && yarn run dev"


-dp 3000:3000   - same as before. Run in detached (background) mode and create a port mapping
-w /app         - sets the “working directory” or the current directory that the command will run from
-v "$(pwd):/app" - bind mount the current directory from the host in the container into the /app directory

node:12-alpine - the image to use. Note that this is the base image for our app from the Dockerfile

sh -c "yarn install && yarn run dev" 
                - the command. We’re starting a shell using sh (alpine doesn’t have bash) and running yarn install to install all dependencies and then running yarn run dev. If we look in the package.json, we’ll see that the dev script is starting nodemon.


docker logs -f <container-id>

# Network


docker network create todo-app

 docker run -d `
     --network todo-app --network-alias mysql `
     -v todo-mysql-data:/var/lib/mysql `
     -e MYSQL_ROOT_PASSWORD=secret `
     -e MYSQL_DATABASE=todos `
     mysql:5.7

    


docker exec -it <mysql-container-id> mysql -u root -p


 PS> docker run -dp 3000:3000 `
   -w /app -v "$(pwd):/app" `
   --network todo-app `
   -e MYSQL_HOST=mysql `
   -e MYSQL_USER=root `
   -e MYSQL_PASSWORD=secret `
   -e MYSQL_DB=todos `
   node:12-alpine `
   sh -c "yarn install && yarn run dev"



## Push to docker repository

docker push docker/getting-started

docker tag getting-started YOUR-USER-NAME/getting-started

docker push YOUR-USER-NAME/getting-started



## Docker Compose

Docker Compose is a tool that was developed to help define and share multi-container applications. 
With Compose, we can create a YAML file `docker-compose.yml` to define the services and with a single command, can spin everything up or tear it all down.

The big advantage of using Compose is you can define your application stack in a file, keep it at the root of your project repo 
(it’s now version controlled), and easily enable someone else to contribute to your project. 

Someone would only need to clone your repo and start the compose app. In fact, you might see quite a few projects on GitHub/GitLab doing exactly this now.


`docker-compose version`

```powershell
PS> docker run -dp 3000:3000 `
  -w /app -v "$(pwd):/app" `
  --network todo-app `
  -e MYSQL_HOST=mysql `
  -e MYSQL_USER=root `
  -e MYSQL_PASSWORD=secret `
  -e MYSQL_DB=todos `
  node:12-alpine `
  sh -c "yarn install && yarn run dev"
```

to `docker-compose.yml` would be:

```
version: "3.7"

services:
  app:
    image: node:12-alpine
    command: sh -c "yarn install && yarn run dev"
    ports:
      - 3000:3000
    working_dir: /app
    volumes:
      - ./:/app
    environment:
      MYSQL_HOST: mysql
      MYSQL_USER: root
      MYSQL_PASSWORD: secret
      MYSQL_DB: todos

  mysql:
    image: mysql:5.7
    volumes:
      - todo-mysql-data:/var/lib/mysql
    environment:
      MYSQL_ROOT_PASSWORD: secret
      MYSQL_DATABASE: todos

volumes:
  todo-mysql-data:

```



The  name for the service (`app`, `mysql`) will automatically become a network alias, which will be useful when defining our MySQL service.


docker-compose up -d

docker-compose down

By default, named volumes in your compose file are NOT removed when running docker-compose down. If you want to remove the volumes, you will need to add the --volumes flag.



## Notes

          <host-port>:<image-port>
--publish 8080:80


# Reference

https://docs.docker.com/engine/reference/commandline/run/

https://github.com/dotnet/dotnet-docker
https://hub.docker.com/_/microsoft-dotnet
