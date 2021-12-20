# SmartLockDemo.Business

It is a .NET 5 class library that handles the business rules of the _smart-lock-demo_ system. It declares all its dependencies with the `Business.ModuleDescriptor` so that it can be loaded by an [IoC container](https://www.martinfowler.com/articles/injection.html).

![BusinessServices](https://user-images.githubusercontent.com/54269270/146840706-3b24dcf2-42b6-413f-ba33-5fbc07d4a822.jpg)

There are only two services offered by the Business tier through **Business.ModuleDescriptor**; `IAdministrationService`, `IUserService`. These services implement business rules using the services of the Data and Infrastructure layer. In addition, it transforms the models it uses from the lower layers into business models through [AutoMapper](https://docs.automapper.org/en/stable/Getting-started.html) in order to service its own services with **complete independence**.
