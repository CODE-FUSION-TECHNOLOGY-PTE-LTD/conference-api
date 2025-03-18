@echo off
echo Starting all projects...

start dotnet run --project ApiGateway/ApiGateway.csproj
start dotnet run --project src/ConferenceApi/ConferenceApi.csproj
start dotnet run --project src/Payment.Api/Payment.Api.csproj
start dotnet run --project src/RegisterApi/RegisterApi.csproj


echo All projects started.
pause