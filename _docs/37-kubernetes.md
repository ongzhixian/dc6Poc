# Kubernetes

docker login

docker tag pizzafrontend [YOUR DOCKER USER NAME]/pizzafrontend
docker tag pizzabackend [YOUR DOCKER USER NAME]/pizzabackend

docker push [YOUR DOCKER USER NAME]/pizzafrontend
docker push [YOUR DOCKER USER NAME]/pizzabackend

kubectl get pods

kubectl apply -f backend-deploy.yml
kubectl apply -f frontend-deploy.yml

kubectl scale --replicas=5 deployment/pizzabackend
kubectl scale --replicas=1 deployment/pizzabackend


Run `backend-deploy.yml`. 
It will download the image from Docker Hub and create the container.
`kubectl apply` command is asynchronous; 
The kubectl apply command will return quickly. But the container creation may take a while. To view the progress, use `kubectl get pods`




```
---
apiVersion: apps/v1
kind: Deployment
metadata:
  name: pizzabackend
spec:
  replicas: 1
  template:
    metadata:
      labels:
        app: pizzabackend
    spec:
      containers:
      - name: pizzabackend
        image: [YOUR DOCKER USER NAME]/pizzabackend:latest
        ports:
        - containerPort: 80
        env:
        - name: ASPNETCORE_URLS
          value: http://*:80
  selector:
    matchLabels:
      app: pizzabackend
---
apiVersion: v1
kind: Service
metadata:
  name: pizzabackend
spec:
  type: ClusterIP
  ports:
  - port: 80
  selector:
    app: pizzabackend
```

```
---
apiVersion: apps/v1
kind: Deployment
metadata:
  name: pizzafrontend
spec:
  replicas: 1
  template:
    metadata:
      labels:
        app: pizzafrontend
    spec:
      containers:
      - name: pizzafrontend
        image: [YOUR DOCKER USER NAME]/pizzafrontend
        ports:
        - containerPort: 80
        env:
        - name: ASPNETCORE_URLS
          value: http://*:80
        - name: backendUrl
          value: http://pizzabackend
  selector:
    matchLabels:
      app: pizzafrontend
---
apiVersion: v1
kind: Service
metadata:
  name: pizzafrontend
spec:
  type: LoadBalancer
  ports:
  - port: 80
  selector:
    app: pizzafrontend
```


# Secrets

```yml
apiVersion: v1
kind: Secret
metadata:
  name: test-secret
data:
  username: bXktYXBw
  password: Mzk1MjgkdmRnN0pi
```

Note: Values for data field need to be encoded in base64.


kubectl apply -f https://k8s.io/examples/pods/inject/secret.yaml

kubectl get secret test-secret

kubectl describe secret test-secret

Create secret directly
kubectl create secret generic test-secret  --from-literal='username=my-app' --from-literal='password=39528$vdg7Jb'


```yml
apiVersion: v1
kind: Pod
metadata:
  name: secret-test-pod
spec:
  containers:
    - name: test-container
      image: nginx
      volumeMounts:
        # name must match the volume name below
        - name: secret-volume
          mountPath: /etc/secret-volume
  # The secret data is exposed to Containers in the Pod through a Volume.
  volumes:
    - name: secret-volume
      secret:
        secretName: test-secret
```

This maps each secret as individual file under the mountPath:
Meaning `ls /etc/secret-volume` will list:
1.  password    (/etc/secret-volume/password)
2.  username    (/etc/secret-volume/username)


Environment variables using Secret data 
                              <secret-name>               <secret-key>
kubectl create secret generic backend-user --from-literal=backend-username='backend-admin'


```yml:read secret into environment variable
apiVersion: v1
kind: Pod
metadata:
  name: env-single-secret
spec:
  containers:
  - name: envars-test-container
    image: nginx
    env:
    - name: SECRET_USERNAME
      valueFrom:
        secretKeyRef:
          name: backend-user
          key: backend-username
```

Assign the backend-username value defined in the Secret to the SECRET_USERNAME environment variable in the Pod specification.


https://kubernetes.io/docs/tasks/inject-data-application/_print/


# Reference

https://docs.microsoft.com/en-us/learn/modules/dotnet-deploy-microservices-kubernetes/

https://jamesdefabia.github.io/docs/user-guide/kubectl-cheatsheet/
