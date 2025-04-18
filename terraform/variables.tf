variable "resource_group_name" {
  description = "Name of the Azure resource group"
  type        = string
  default     = "DemoMerchStoreRG"
}

variable "location" {
  description = "Azure region where resources will be created"
  type        = string
  default     = "swedencentral"
}

variable "environment_name" {
  description = "Name of the Container App Environment"
  type        = string
  default     = "DemoMerchStoreEnv"
}

variable "app_name" {
  description = "Name of the Container App"
  type        = string
  default     = "demomerchstoreapp"
}

variable "container_image" {
  description = "Docker image for the container app"
  type        = string
  default     = "dennisbyberg/cloud-applications-advanced:main"
}

variable "container_port" {
  description = "Container port to expose"
  type        = number
  default     = 8080
}

variable "cpu" {
  description = "CPU allocation for the container"
  type        = number
  default     = 0.25
}

variable "memory" {
  description = "Memory allocation for the container"
  type        = string
  default     = "0.5Gi"
}