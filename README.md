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
 dotnet tool install -g Microsoft.dotnet-httprepl;
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

## add

```shell
dotnet add package Microsoft.EntityFrameworkCore.Sqlite;
dotnet add package Microsoft.EntityFrameworkCore.Design;
dotnet tool install --global dotnet-ef;
```

## create db tables

```shell
using ContosoPizza.Data;
```

## apply create

```shell
dotnet ef database update --context PizzaContext
```

## revisions

```shell
dotnet ef migrations add ModelRevisions --context PizzaContext
```

## update

```shell
dotnet ef database update --context PizzaContext
```

## Build scafolding

```shell
dotnet ef dbcontext scaffold "Data Source=./Promotions/Promotions.db" Microsoft.EntityFrameworkCore.Sqlite --context-dir ./Data --output-dir .\Models
```

-

```
The preceding command:

Scaffolds a DbContext and model classes using the provided connection string.
Specifies the Microsoft.EntityFrameworkCore.Sqlite database provider should be used.
Specifies directories for the resulting DbContext and model classes.
```

pref set editor.command.default "/System/Applications/TextEdit.app/Contents/MacOS/TextEdit"