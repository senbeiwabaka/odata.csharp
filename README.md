# odata.csharp
Example OData using C#


## DBs

You can use:

* Microsoft SQL Server 
* Postgres
* SQL Lite
* In memory

The default is In Memory.

To use one of the databases you will need to set the connection for the app settings connection string to grab it from.

* `MSSQL` (Microsoft SQL Server)
* `Postgres` (Postgres)
* `UseSqllite` (SQL Lite) to true since it will use sqlite in memory vs a file. You could use a file but will have to modify code yourself