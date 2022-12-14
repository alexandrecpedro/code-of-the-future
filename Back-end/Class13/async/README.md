#### Class 13 | Idempotence, Circuit Breaker and Telemetry
    -   Idempotency in commands
        -   Definition
            -   https://xuenqui.medium.com/idempot%C3%AAncia-uma-boa-pr%C3%A1tica-a-se-utilizar-em-servi%C3%A7os-rest-633c38f4d7c0
            -   Command
                -   A request to the system to perform an action that will change the state of the system
                -   Can be accepted or refused
                -   Must be executed only once by a single receiver (unique action)
                -   Obs.: An event can be processed more than once by multiple clients (multiple stakeholders)
                -   Examples of commands (below)
```c#
    namespace Ordering.Commands;
    
    public class CreateOrderCommand : IRequest<bool>
    {
        public CreateOrderCommand() { }

        public CreateOrderCommand(Guid idempotencyId, List<CreateOrderCommand> items)
        {
            IdempotencyId = idempotencyId;
            Items = items;
            // CustomerId = customerId;
            // CustomerName = customerName;
            // CustomerEmail = customerEmail;
            // CustomerPhone = customerPhone;
            // CustomerAddress = customerAddress;
            // CustomerAdditionalCustomer = customerAdditionalCustomer;
            // CustomerDistrict = customerDistrict;
            // CustomerCity = customerCity;
            // CustomerState = customerState;
            // CustomerZipCode = customerZipCode;
        }

        Guid IdempotencyId { get; set; } = default;
        List<CreateOrderCommand> Items { get; set; } = default;
    }
```
    -   Circuit Breaker concept
        -   Documentation
            -   https://learn.microsoft.com/pt-br/dotnet/architecture/microservices/implement-resilient-applications/
    -   Setting Polly for HTTP Requests
        -   Reference
            -   https://www.iteris.com.br/blog/resiliencia-em-aplicacoes-net-core-com-polly/
        -   Polly library
            -   https://github.com/App-vNext/Polly
```c#
    // At MVC.csproj
    <PackageReference Include="Polly" Version="7.2.3" />
    <PackageReference Include="Polly.Extensions.Http" Version="3.0.0" />

    // At Startup.cs
    services.AddHttpClient<IBasketService, BasketService>()
            .AddPolicyHandler(GetRetryPolicy()) //any oscillation will not bring down the app
            .AddPolicyHandler(GetCircuitBreakerPolicy()) //the circuit is opened to interrupt the application (there was a failure)
```
    -   Demonstrating Circuit Breaker and retries with Polly
    -   Presenting Telemetry
        -   Definition
            -   Remote measurement
                -   Data transfer = wireless, optical fiber, electrical network)
                -   Measuring equipment
        -   Reference
            -   https://opentelemetry.io/
<div align = 'center' justify-content = 'space-around'>
  <img width="1604" alt="Microsservices Project - telemetry" src="./GroceryStore/anatomia.png">
</div>

```
    -   Using telemetry in ASP.NET Core I
        -   Documentation
            -   https://learn.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core?tabs=netcorenew%2Cnetcore6
        -   Create 2 projects (WebAPI): ServiceA (client), ServiceB (provider)
        -   RestSharp (ServiceA - client)
            -   https://www.nuget.org/packages/RestSharp/109.0.0-preview.1
    -   Using telemetry in ASP.NET Core II
        -   OpenTelemetry (ServiceA, ServiceB)
            -   https://www.nuget.org/packages/OpenTelemetry/1.4.0-rc.1
            -   https://www.nuget.org/packages/OpenTelemetry.Api/1.4.0-rc.1
            -   https://www.nuget.org/packages/OpenTelemetry.Extensions.Hosting/1.4.0-rc.1
            -   https://www.nuget.org/packages/OpenTelemetry.Instrumentation.Http/1.0.0-rc9.10
            -   https://www.nuget.org/packages/OpenTelemetry.Instrumentation.AspNetCore/1.0.0-rc9.10
            -   https://www.nuget.org/packages/OpenTelemetry.Exporter.Console/1.4.0-rc.1
    -   Telemetry: Introduction to Jaeger
        -   Documentation
            -   https://www.jaegertracing.io/
        -   OpenTelemetry (ServiceA, ServiceB)
            -   https://www.nuget.org/packages/OpenTelemetry.Exporter.Jaeger/1.4.0-rc.1
```