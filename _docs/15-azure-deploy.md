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
