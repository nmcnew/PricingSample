# Simple Pub/Sub Request-Response Setup

This should be fairly straight-forward.

## Running the application
1. From the solution level directory run `dotnet restore` and `dotnet build` (ensure that you have [dotnet 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2))
2. Startup an instance of each application 
    * the pricers need a unique identifier as a running argument (ie `dotnet run -- luke`)
3. Watch the magic
