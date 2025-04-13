#!/bin/bash

#---------- GLOBAL VARIABLES -------------
RESOURCE_GROUP_TO_DELETE="DockerDemoRG"
SLEEP_INTERVAL=5
SPINNER_DELAY=0.1

#---------- FUNCTIONS -------------
# Display a spinner for long-running operations
spinner() {
    local spinstr='|/-\'
    while true; do
        for i in $(seq 0 3); do
            printf " [%c]  " "${spinstr:i:1}"
            sleep $SPINNER_DELAY
            printf "\b\b\b\b\b\b"
        done
    done
}

# Stop the spinner process safely
stop_spinner() {
    if [[ -n $1 ]]; then
        kill $1 2>/dev/null || true
    fi
}

# Check if a resource group exists
check_resource_group() {
    local rg_name=$1
    az group show --name "$rg_name" &>/dev/null
    return $?
}

# Get the count of resources in a resource group
get_resource_count() {
    local rg_name=$1
    az resource list \
        --resource-group "$rg_name" \
        --query "length([])" \
        --output tsv 2>/dev/null || echo "0"
}

# Delete a resource group and monitor progress
delete_resource_group() {
    local rg_name=$1
    local initial_count=$(get_resource_count "$rg_name")
    local previous_count=-1

    echo "Starting deletion of resource group $rg_name with $initial_count resources."

    # Initiate deletion
    az group delete \
        --name "$rg_name" \
        --yes \
        --no-wait

    echo "Deletion initiated. Monitoring progress..."

    # Start the spinner in the background
    spinner &
    local spinner_pid=$!

    # Monitor deletion progress
    while check_resource_group "$rg_name"; do
        local resource_count=$(get_resource_count "$rg_name")

        # If the resource count has changed, print the new count
        if [[ "$resource_count" != "$previous_count" ]]; then
            echo "Resources left to delete: $resource_count"
            previous_count=$resource_count
        fi

        sleep $SLEEP_INTERVAL
    done

    # Stop the spinner
    stop_spinner $spinner_pid

    echo "Resource group $rg_name has been deleted."
}

#---------- MAIN SCRIPT -------------
# Check if the resource group exists
if ! check_resource_group "$RESOURCE_GROUP_TO_DELETE"; then
    echo "Resource group $RESOURCE_GROUP_TO_DELETE does not exist."
    exit 0
fi

# Delete the resource group and monitor progress
delete_resource_group "$RESOURCE_GROUP_TO_DELETE"

echo "Cleanup complete!"
