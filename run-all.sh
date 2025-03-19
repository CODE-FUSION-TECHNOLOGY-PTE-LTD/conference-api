#!/bin/bash
echo "Starting all projects..."

# Start each project
dotnet run --project ApiGateway/ApiGateway.csproj &
dotnet run --project src/ConferenceApi/ConferenceApi.csproj &
dotnet run --project src/Payment.Api/Payment.Api.csproj &
dotnet run --project src/RegisterApi/RegisterApi.csproj &

# Wait for all processes to finish
wait

echo "All projects started."
