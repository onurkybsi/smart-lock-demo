# smart-lock-demo

_smart-lock-demo_ represents a demo system for a cloud-based smart lock system.

## Installation

Use the [docker-compose](https://docs.docker.com/compose/) to install _smart-lock-demo_

```bash
docker-compose up
```

The services provided by _SmartLockDemo.WebAPI_ can be used with the Swagger interface at http://localhost:8000/swagger/index.html

## Architecture

![Architecture](https://user-images.githubusercontent.com/54269270/146815651-b3dc86e6-fc90-4f12-ad62-aeb4032be4a7.png)

_smart-lock-demo_ is designed according to n-tier architecture. As can be seen from the drawing above, the system is built on 4 layers: `WebAPI`, `Business`, `Data` and `Infrastructure`. All layers specify all their dependencies using _KybInfrastructure's_ [ModuleDescriptor](https://github.com/onurkybsi/KybInfrastructure/tree/master/KybInfrastructure.Core/ModuleDescriptor). The _WebAPI_ layer provides **_absolute independence_** between all layers in the system by resolving the ModuleDescriptor's of all layers.

# WebAPI Layer

It has been developed using ASP.NET Core 5 framework on the WebAPI [RESTful concept](https://en.wikipedia.org/wiki/Representational_state_transfer). Its main purpose is to provide _smart-lock-demo_ services to REST clients, also it compiles an [IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=dotnet-plat-ext-6.0) to manage all dependencies in the system and logs the all requests it processes. [Read More](https://github.com/onurkybsi/smart-lock-demo/tree/master/SmartLockDemo.WebAPI)

# Business Layer

It is the layer where the business rules are handled. It acquires and manipulates the necessary data during business rules using the _Data_ layer. It shares its interfaces for the use of the _WebAPI_ layer. [Read More](https://github.com/onurkybsi/smart-lock-demo/tree/master/SmartLockDemo.Business)

# Data Layer

Connects to [SQL Server](https://en.wikipedia.org/wiki/Microsoft_SQL_Server) database and [Redis](https://redis.io/topics/introduction) distributed memory cache. It provides various services for storing and manipulating the data of the system. [Read More](https://github.com/onurkybsi/smart-lock-demo/tree/master/SmartLockDemo.Data)

# Infrastructure

It contains various functionalities that are independent of the smart-lock-demo system, but are also used by the layers of the smart-lock-demo system. [Read More](https://github.com/onurkybsi/smart-lock-demo/tree/master/SmartLockDemo.Infrastructure)
