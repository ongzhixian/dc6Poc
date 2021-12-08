# Azure Web App


## Commands

List AppServices

`az webapp list  -o table`

Deploy WAR

`az webapp deploy --resource-group ResouceGroup --name AppName --src-path SourcePath --type war --async IsAsync`

Deploy file

`az webapp deploy --resource-group ResouceGroup --name AppName --src-path SourcePath --type static --target-path staticfiles/test.txt`


Resource group: dn6poc-travel-func-rg
Region:         southeastasia
Storage name    dn6poctravelfuncstore
App name:       dn6poc-travel-app


(Not sure why there's a "conflict"; KIV -- manually create the webapp in Azure Portal for now)

`az webapp create -g dn6poc-travel-func-rg -p SoutheastAsiaPlan -n dn6poc-travel-app`

There's no "folder" type deployment, so we should always zip the folder and deploy the zip file.

`az webapp deploy --resource-group dn6poc-travel-func-rg --name dn6poc-travel-app --src-path "D:\src\github\nglearn\dist\travel-app.zip"`


Get list of AppService plans:
`az appservice plan list -o table`