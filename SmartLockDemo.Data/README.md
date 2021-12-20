# SmartLockDemo.Data

It provides the necessary services for storing and manipulating the data of the system using [SQL Server](https://en.wikipedia.org/wiki/Microsoft_SQL_Server) and [Redis](https://redis.io/)
distributed cache.

![DbERD](https://user-images.githubusercontent.com/54269270/146842980-dd6cfdf6-c20a-414a-9207-5282760bf290.jpg)

Due to the nature of the model objects in the designed system, the relational database was deemed appropriate and the database was structured according to the ERD diagram above. Due to its high compatibility with the [.NET framework](https://docs.microsoft.com/en-us/dotnet/framework/get-started/overview), [SQL Server](https://en.wikipedia.org/wiki/Microsoft_SQL_Server) was preferred among relational databases.

![DataLayer](https://user-images.githubusercontent.com/54269270/146844839-ab2ab99a-c025-4334-b3cc-a8db0d0ddd60.png)

[UnitOfWork](https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application) design pattern is used together with Repository design pattern in order to ensure consistency in transactions in the data layer.
Due to the system's needs cloud supporting and data consistency, distributed caching was preferred rather than in-memory caching, and Redis was included in the system.
