# Util Application Logging

## Purpose
* Helper Utiltiy for Standardized Application Logging through Serilog


## Usage

* Install NuGet Package in an application
* Set LogLevel and Log Output Type (Console, FileLog, ElsaticLog) in appsettings.json as shown below
  * ConsoleLog -> Enabled (set log output as console). For Cloud Applications, it should be true, so all logs can be scrapped/read through prometheus and push to Grafana
   * FileLog -> Enabled (set log output as file). For IIS hosted applications, it should be true, so all logs can be created at logs\File folder (performance-demo-api-development-20220629, usage-demo-api-development-20220629)
   * ElasticLog -> Enabled (set log output to Elastic). This can be used to directly output logs to Elastic by providing valid Url.

```json
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "ConsoleLog": {
      "Enabled": true
    },
    "FileLog": {
      "Enabled": false
    },
    "ElasticLog": {
      "Enabled": false,
      "Url": "http://localhost:9200"
    }
  },
  "AppSettings": {
    "ApplicationName": "asm-api",
    "ApplicationVersion": "1.0.0",
    "EnablePerformanceFilterLogging": true
  }
```

```c
_logger.LogInformationExtension($"Get Product By Id: {id}");
```

## Extension Methods

* LogTraceExtension
* LogDebugExtension
* LogInformationExtension
* LogWarningExtension
* LogErrorExtension
* LogCriticalExtension
* LogRoutePerformance
* LogUnauthorizedAccess

** Note: We have implemented extension methods to improve performance in logging as per Microfot's Guideline.

* High-performance logging with LoggerMessage in ASP.NET Core - https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/loggermessage
* LoggingHelpers Performance Measurement - https://www.stevejgordon.co.uk/high-performance-logging-in-net-core


## Output

* Information Log

```json
{
   "Timestamp":"2022-07-20T17:53:34.4987477Z",
   "LogLevel":"Information",
   "Message":"Get Product By Id: 1",
   "ActionName":"Demo.Api.Controllers.ProductController.GetById (Demo.Api)",
   "RequestPath":"/api/v1.0/products/1",
   "ApplicationName":"Demo.Api",
   "ApplicationVersion":"1.0",
   "Environment":"Development",
   "LoggerName":"Demo.Util",
   "RequestMethod":"GET",
   "Referer":"https://localhost:5001/swagger/index.html",
   "CorrelationId":"5761bcd4-b19b-4cd7-a055-3baec12ecf71",
   "HostName":"{HostValue}",
   "ClientIp":"::1",
   "ClientAgent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.114 Safari/537.36 Edg/103.0.1264.62",
   "ProcessId":24788,
   "ProcessName":"Demo.Api",
   "ThreadId":12,
   "ServiceName":"products",
   "ServiceVersion":"1.0"
}
```

* Error Log

```json
{
   "Timestamp":"2022-07-20T17:53:34.6974606Z",
   "LogLevel":"Error",
   "Message":"Exception Occurred: Test Exception -- ErrorId: 9f517917-56a0-4276-af20-82571ac2a526",
   "ErrorId":"9f517917-56a0-4276-af20-82571ac2a526",
   "ErrorMessage":"Test Exception",
   "ExceptionName":"ApplicationException",
   "ExceptionSource":"Demo.Business",
   "StackTrace":[
      "   at Demo.Business.Services.ProductService.GetById(Int32 id) in C:\\Vishal\\Projects\\Demo\\src\\Demo.Business\\Services\\ProductService.cs:line 31",
      "   at Demo.Api.Controllers.ProductController.GetById(Int32 id) in C:\\Vishal\\Projects\\Demo\\src\\Demo.Api\\Controllers\\ProductController.cs:line 65",
      "   at lambda_method9(Closure , Object )",
      "   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)",
      "   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Logged|12_1(ControllerActionInvoker invoker)",
      "   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)",
      "   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)",
      "   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)",
      "   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()",
      "--- End of stack trace from previous location ---",
      "   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|20_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)",
      "   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Logged|17_1(ResourceInvoker invoker)",
      "   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Logged|17_1(ResourceInvoker invoker)",
      "   at Microsoft.AspNetCore.Routing.EndpointMiddleware.<Invoke>g__AwaitRequestTask|6_0(Endpoint endpoint, Task requestTask, ILogger logger)",
      "   at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)",
      "   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)",
      "   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)",
      "   at Demo.Api.Middleware.ApiExceptionMiddleware.Invoke(HttpContext context) in C:\\Vishal\\Projects\\Demo\\src\\Demo.Api\\Middleware\\ApiExceptionMiddleware.cs:line 25"
   ],
   "RequestPath":"/api/v1.0/products/1",
   "ApplicationName":"Demo.Api",
   "ApplicationVersion":"1.0",
   "Environment":"Development",
   "LoggerName":"Demo.Util",
   "RequestMethod":"GET",
   "Referer":"https://localhost:5001/swagger/index.html",
   "CorrelationId":"5761bcd4-b19b-4cd7-a055-3baec12ecf71",
   "HostName":"{HostValue}",
   "ClientIp":"::1",
   "ClientAgent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.114 Safari/537.36 Edg/103.0.1264.62",
   "ProcessId":24788,
   "ProcessName":"Demo.Api",
   "ThreadId":12,
   "ServiceName":"products",
   "ServiceVersion":"1.0"
}
```

* Performance Log

```json
{
   "Timestamp":"2022-07-20T18:05:02.7626795Z",
   "LogLevel":"Information",
   "Message":"/api/v1.0/products/1 - GET - code took 143 milliseconds.",
   "Duration":143,
   "ActionName":"Demo.Api.Controllers.ProductController.GetById (Demo.Api)",
   "RequestPath":"/api/v1.0/products/1",
   "ApplicationName":"Demo.Api",
   "ApplicationVersion":"1.0",
   "Environment":"Development",
   "LoggerName":"Demo.Util",
   "RequestMethod":"GET",
   "Referer":"https://localhost:5001/swagger/index.html",
   "CorrelationId":"3a5f9835-fb58-416d-a16f-dedb98929c9b",
   "HostName":"{HostValue}",
   "ClientIp":"::1",
   "ClientAgent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.114 Safari/537.36 Edg/103.0.1264.62",
   "ProcessId":36136,
   "ProcessName":"Demo.Api",
   "ThreadId":5,
   "ServiceName":"products",
   "ServiceVersion":"1.0"
}
```

* Unauthorized Access Log

```json
{
   "Timestamp":"2022-07-20T18:21:41.3347276Z",
   "LogLevel":"Fatal",
   "Message":"Unauthorized Access - Authentication - Token is NULL",
   "EventType":"Authentication",
   "Description":"Token is NULL",
   "ActionName":"Demo.Api.Controllers.ProductController.GetById (Demo.Api)",
   "RequestPath":"/api/v1.0/products/1",
   "ApplicationName":"Demo.Api",
   "ApplicationVersion":"1.0",
   "Environment":"Development",
   "LoggerName":"Demo.Util",
   "RequestMethod":"GET",
   "Referer":"https://localhost:5001/swagger/index.html",
   "CorrelationId":"5bc969d6-7283-4440-90e8-2a21fd8e7d76",
   "HostName":"{HostName}",
   "ClientIp":"::1",
   "ClientAgent":"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.5060.114 Safari/537.36 Edg/103.0.1264.62",
   "ProcessId":18760,
   "ProcessName":"Demo.Api",
   "ThreadId":9,
   "ServiceName":"products",
   "ServiceVersion":"1.0"
}
```