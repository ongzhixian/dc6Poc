# Github Actions

Go to the Github repository, and add (commit) a workflow in the Actions section.
Picking the .NET starter workflow.


## With Azure

Actions:
    `azure/login@v1`

    The Action supports two different ways of authentication with Azure:
    1.  Azure Service Principal with secrets. 
    2   OpenID Connect (OIDC) method of authentication using Azure Service Principal with a Federated Identity Credential.


## Create/Configure a Azure Service Principal with a secret:

Use `az ad sp`
This command manage Azure Active Directory service principals for automation authentication

Note: RBAC => Resource Based Access Control

```cmd:azure CLI

az ad sp create-for-rbac --name "myApp" --role contributor \
                        --scopes /subscriptions/{subscription-id}/resourceGroups/{resource-group} \
                        --sdk-auth
                        
  # Replace {subscription-id}, {resource-group} with the subscription, resource group details

  # The command should output a JSON object similar to this:

  {
    "clientId": "<GUID>",
    "clientSecret": "<GUID>",
    "subscriptionId": "<GUID>",
    "tenantId": "<GUID>",
    (...)
  }
  
```




```yaml
jobs:

  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
    
    - uses: azure/login@v1
      with:
        creds: ${{ secrets.AZURE_CREDENTIALS }}
    
    - run: |
        az webapp list --query "[?state=='Running']"
```


## Reference

https://github.com/marketplace/actions/azure-login

https://github.com/Azure/functions-action

https://github.com/Azure/webapps-deploy

https://docs.microsoft.com/en-US/cli/azure/ad/sp?view=azure-cli-latest#commands

https://github.com/actions

https://docs.github.com/en/actions/advanced-guides/storing-workflow-data-as-artifacts#downloading-or-deleting-artifacts

https://focisolutions.com/2020/04/github-actions-deploying-an-angular-app/
