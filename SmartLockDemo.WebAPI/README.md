# SmartLockDemo.WebAPI

It is a web API that implements the [RESTful](https://en.wikipedia.org/wiki/Representational_state_transfer) concept using the [ASP.NET Core 5](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0) framework.

![WebAPIArchitecture](https://user-images.githubusercontent.com/54269270/146836389-0018d1ab-489e-49b8-af94-0e59fae521aa.png)

- It configures the system by loading [appsettings.json](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-5.0) files and environment variables that are declared separately for each environment (Development, Staging, Production).
- It builds an [IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=dotnet-plat-ext-6.0) through the layers' [ModuleDescriptor](https://github.com/onurkybsi/KybInfrastructure/tree/master/KybInfrastructure.Core/ModuleDescriptor)'s so that all sublayers' dependencies can be resolved.
- Authenticates REST clients with [JWT](https://jwt.io/introduction) based _Bearer Authorization_.
- Logs all received requests using [ELK](https://www.elastic.co/what-is/elk-stack).
- It forwards the requests of REST clients, which is validated by Bearer Authorization, to the Business layer for processing according to business rules.
- It presents the system's public services via the [Swagger](https://swagger.io/) interface at the `/swagger/index.html` endpoint.
