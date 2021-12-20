# SmartLockDemo.WebAPI

It is a Web API that implements the RESTful concept using the ASP.NET Core 5 framework.

- It configures the system by loading appsettings.json files and environment variables that are declared separately for each environment (Development, Staging, Production).
- It builds an IServiceCollection through the layers' ModuleDescriptor's so that all sublayers' dependencies can be resolved.
- Authenticates REST clients with Bearer Authentication.
- Logs all received requests using ELK.
- It forwards the requests of REST clients, which is validated by Bearer Authentication, to the Business layer for processing according to business rules.
- It presents the system's public services via the Swagger interface at the /swagger/index.html endpoint.
