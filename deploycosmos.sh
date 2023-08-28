az account clear
az login
subs=$(az account list --query '[].id' --output tsv)
echo $subs
#az account set --subscription $subs
rgname=$(az group list --subscription $subs --query '[].name' --output tsv)
echo $rgname
resourcegp=$rgname
regiao='West Us'
cosmoAccount='mod03dp420t1'
cosmoBanco='dbproduct'
cosmoContainer='cont'

# Crie uma conta do Cosmos DB com a API NoSQL
echo 'Criando um banco de dados cosmo... Aguarde'
az cosmosdb create --name $cosmoAccount --resource-group $resourcegp --subscription $subs --locations 'RegionName=West Us'
echo 'Banco Criado'
# Liste as APIs disponíveis para a conta do Cosmos DB
endpoint=$(az cosmosdb show --name $cosmoAccount --resource-group $resourcegp --subscription $subs --query "documentEndpoint" --output tsv)
echo $endpoint
az cosmosdb keys list --name $cosmoAccount --resource-group $resourcegp --subscription $subs
