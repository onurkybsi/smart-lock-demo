# SmartLockDemo.WebAPI

It is a web API that implements the [RESTful](https://en.wikipedia.org/wiki/Representational_state_transfer) concept using the [ASP.NET Core 5](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-5.0) framework.

![WebAPIArchitecture](https://user-images.githubusercontent.com/54269270/146836748-e1fc31fe-a6a4-4948-9580-a7668f970b1d.png)

- It configures the system by loading [appsettings.json](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-5.0) files and environment variables that are declared separately for each environment (Development, Staging, Production).
- It builds an [IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection?view=dotnet-plat-ext-6.0) through the layers' [ModuleDescriptor](https://github.com/onurkybsi/KybInfrastructure/tree/master/KybInfrastructure.Core/ModuleDescriptor)'s so that all sublayers' dependencies can be resolved.
- Authenticates REST clients with [JWT](https://jwt.io/introduction) based _Bearer Authorization_.
- Logs all received requests using [ELK](https://www.elastic.co/what-is/elk-stack).
- It forwards the requests of REST clients, which is validated by Bearer Authorization, to the Business layer for processing according to business rules.
- It presents the system's public services via the [Swagger](https://swagger.io/) interface at the `/swagger/index.html` endpoint.

## Some important services

#### CreateUser service

#### Request

`POST /administration/createuser`

```bash
$ curl -X POST "http://localhost:8000/administration/createuser" -H  "accept: */*" -H  "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IjEiLCJlbWFpbCI6IjEiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2NDAwMzY5MjMsImV4cCI6MTY0MDA4MDEyMywiaWF0IjoxNjQwMDM2OTIzfQ.OrGwRstua5ybvalEGmdD9tO3ca9CKzUOz-m9DgxByfc" -H  "Content-Type: application/json" -d "{\"email\":\"someemail@gmail.com\",\"password\":\"Strongpassword123!\"}"
```
#### Response
  ```
  HTTP/1.1 201 CREATED
  Date: Mon20 Dec 2021 21:49:51 GMT
  Location: /administration/createuse
  {
    "isSuccessful": true,
    "message": null
  }
```

#### TagUser service

#### Request

`POST /administration/taguser`

```bash
$ curl -X POST "http://localhost:8000/administration/taguser" -H  "accept: */*" -H  "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IjEiLCJlbWFpbCI6IjEiLCJyb2xlIjoiQWRtaW4iLCJuYmYiOjE2NDAwMzY5MjMsImV4cCI6MTY0MDA4MDEyMywiaWF0IjoxNjQwMDM2OTIzfQ.OrGwRstua5ybvalEGmdD9tO3ca9CKzUOz-m9DgxByfc" -H  "Content-Type: application/json" -d "{\"userId\":2,\"tagId\":1}"
```
#### Response
  ```
  HTTP/1.1 201 CREATED
  Date: Mon20 Dec 2021 22:00:46 GMT
  Location: /administration/createuse
  {
    "isSuccessful": true,
    "message": null
  }
```

#### DoorAccess service

#### Request

`GET /dooraccess/1`

```bash
$ curl -X GET "http://localhost:8000/dooraccess/1" -H  "accept: */*" -H  "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJRCI6IjIiLCJlbWFpbCI6IjIiLCJyb2xlIjoiVXNlciIsIm5iZiI6MTY0MDAzNzc2NCwiZXhwIjoxNjQwMDgwOTY0LCJpYXQiOjE2NDAwMzc3NjR9.0oBJtS38pV1tjVUC-aXRgx-4VxtpZwPU6TjA-8sXF30"
```
#### Response
  ```
  HTTP/1.1 200 ok
  Date: Mon20 Dec 2021 22:03:03 GMT
  Content-Length: 0
```
