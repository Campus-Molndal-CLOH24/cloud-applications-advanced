#!/bin/bash

#---------- GLOBAL VARIABLES -------------
resource_group="DemoMerchStore"
location="swedencentral"
env_name="DemoMerchStoreEnv"
app_name="demomerchstoreapp"
app_port="8080"
image="ghcr.io/campus-molndal-cloh24/cloud-applications-advanced:latest"

az group create \
    --location $location \
    --name $resource_group

az containerapp env create \
    --name $env_name \
    --resource-group $resource_group \
    --location $location

az containerapp create \
    --name $app_name \
    --resource-group $resource_group \
    --image $image \
    --environment $env_name \
    --target-port $app_port \
    --ingress external --query properties.configuration.ingress.fqdn
