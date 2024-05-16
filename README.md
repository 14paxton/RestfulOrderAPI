# Things I Ran Out Of Time For

- Clean code
  - Use Repository class clean up db business logic for services
  - Create db helper Interface for repetitive db functions i.e. Delete(create batch delete using entity frame work ExecuteDelete), Create, Update(create a batch update)
  - logic dealing with Customer should be moved from OrderService to CustomerService
  - In tests service that is not being tested shoud be mocked, tests should only test within scope , and need to trust injected service is returning what it is supposed to, which is why good tests are paramount
  - remove business logic from controller, also i don't love services being injected into a controller, ideally client -> controller -> repository(more db query and LINQ) -> service (business logic)
  - Use actually testing framework to test controllers, ( I firmly believe tests are there to save developers from pushing changes that affect scenarios that were not thought of due to tunnel vision)
  - Add more models to better show clever queries on intricate relations
- Create basic front end form using react
    - Deploy to Netlify
  - Deploy backend to Azure
    - Had troubles with "Free Tier" and became more time consuming than necessary as it wasn't a requirement, just would have been a good demonstration of what I have done, also had trouble with AWS for some reason (again with the free tier) the app deployment demanded I create a load balancer, but it would never finish building the creating spinner spun for a whole day with no error   ¯\_(ツ)_/¯ 	

# Create

```shell
dotnet new webapi -controllers -f net8.0.101
```

# Run

```shell
dotnet run --urls=https://localhost:5101
```

# HttpRepl

```shell
::install
 dotnet tool install -g Microsoft.dotnet-httprepl;

::set path to tools
export PATH="$PATH:/Users/bp/.dotnet/tools";

::test web api
httprepl http://localhost:5001

```

## Set text editor for POST

```shell
pref set editor.command.default "/Applications/Visual Studio Code.app/Contents/Resources/app/bin/code"
```

> set default args

   ```shell
        pref set editor.command.default.arguments "--disable-extensions --new-window"
   ```

## list and select controllers

```shell
ls
```

```shell
cd [controller]
```

## POST

```shell
post -h Content-Type=application/json
```

## GET

```shell
get Order
```

> return

```json
[
  {
    "date": "2024-05-14",
    "summary": "order3"
  },
  {
    "date": "2024-05-15",
    "summary": "order5"
  },
  {
    "date": "2024-05-16",
    "summary": "order4"
  },
  {
    "date": "2024-05-17",
    "summary": "order4"
  },
  {
    "date": "2024-05-18",
    "summary": "order5"
  }
]
```

# Entity Framework

## install

```shell
dotnet add package Microsoft.EntityFrameworkCore.Sqlite;
dotnet add package Microsoft.EntityFrameworkCore.Design;
dotnet tool install --global dotnet-ef;
```

## create db tables

> in Program.cs add

```csharp
using RestfulOrderAPI.Data;

///

builder.Services.AddSqlite<OrderContext>("Data Source=RestfulOrderAPI.db");
```

> initial migrations

```csharp
dotnet ef migrations add InitialCreate --context OrderContext
```

## apply create

```shell
dotnet ef database update --context OrderContext
```

## revisions

```shell
dotnet ef migrations add ModelRevisions --context OrderContext
```

## update

```shell
dotnet ef database update --context OrderContext
```

## Build scafolding

```shell
dotnet ef dbcontext scaffold "Data Source=./Customer/Customers.db" Microsoft.EntityFrameworkCore.Sqlite --context-dir ./Data --output-dir .\Models
```

```
The preceding command:

Scaffolds a DbContext and model classes using the provided connection string.
Specifies the Microsoft.EntityFrameworkCore.Sqlite database provider should be used.
Specifies directories for the resulting DbContext and model classes.
```

# Testing

## Use HTTP file for testing controller

> Reference: [HTTP Testing Files](https://www.jetbrains.com/help/idea/exploring-http-syntax.html#use-a-variable-inside-the-request)

[RestfulOrdersAPI.http](RestfulOrdersAPI.http)

# NUnit 4

## Orders

[OrderServiceTest.cs](..%2FRestfulOrderAPI.Tests%2FServices%2FOrderServiceTest.cs)
