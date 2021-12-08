# Azure Deploy

az login
az group create --name dn6poc-travel-func-rg --location southeastasia
az storage account create --name dn6poctravelfuncstore --location southeastasia --resource-group dn6poc-travel-func-rg --sku Standard_LRS
az functionapp create --resource-group dn6poc-travel-func-rg --consumption-plan-location southeastasia --runtime dotnet-isolated --functions-version 4 --name dn6poc-travel-func --storage-account dn6poctravelfuncstore

func azure functionapp publish dn6poc-travel-func

func azure functionapp logstream dn6poc-travel-func

## Remarks

'account_name' must have length less than 24.
Resource group: dn6poc-travel-func-rg
Region:         southeastasia
Storage name    dn6poctravelfuncstore
App name:       dn6poc-travel-func


Storage account name must be between 3 and 24 characters in length and use numbers and lower-case letters only.

## Region

To get a list of regions, run:

`az account list-locations -o table`


## MongoDB Whitelist

In order for Azure Functions to talk to MongoDB, 
you need to whitelist the IP address of your Azure Functions app 
in MongoDb Atlas (Security > Network Access).

To quickly test if everything is working, white list 0.0.0.0/0 (anywhere)

Once you confirm that your MongoDB Atlas is accessible from your Azure functions, 
you can remove 0.0.0.0 and add the more restrictive IP address of your Azure Functions app.

To find the IP address of your Azure Functions app that needs to be whitelist, go the function app.
On the left panel, look under Settings > Networking.
Then find the box titled Outbound Traffic.
It should have a section call Outbound addresses. It should have a bunch of address like the following:

40.119.249.118,
40.119.250.97,
40.119.249.110,
40.119.250.103,
40.119.250.109,
40.119.250.110,

20.195.48.194,
20.195.48.196,
20.195.48.203,
20.195.39.49,
20.195.39.57,
20.195.39.112,

20.43.172.222,
20.43.173.39,
20.43.173.234,
20.43.153.185,
20.43.153.254,
20.43.154.87,
20.43.132.129

Key in every of these!

If you find this to be as tedious as it is, you can enter blocks of IP addresses in CIDR notation like this:
40.119.0.0/16
20.195.0.0/16
20.43.0.0/16

There's also Atlas Administration API (but not sure if it allows you to whitelist IP addresses):
https://docs.atlas.mongodb.com/api/


## Reference

https://www.mongodb.com/blog/post/how-to-integrate-azure-functions-with-mongodb


CIDR calculator
https://www.ipaddressguide.com/cidr
