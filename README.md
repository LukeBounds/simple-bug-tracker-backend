# Simple Bug Tracker - Back End
The backend is built in .NET 8.0, using a Clean Architecture, Minimal Apis, mediator pattern implemented using MediatR, CQRS, Entity Framework Core 8 (code first), SQL Server database, NUnit testing.
The backend was built in Visual Studio 2022 version 17.9.2.
I chose not to implement the repository pattern, because I'm not convinced it adds much value when using Entity Framework.

I have taken this approach to demonstrate I'm comfortable in multiple architectures - it is different to the approach I use for https://ropeparison.azurewebsites.net/.
Ropeparison uses a layered architecture, with controllers and injected services. It does implement the repository pattern.

I did not have time to write all of the neccessary unit tests, some are missing for the Bugs.

Notes on setting up the database are included in the Infrastructure project ReadMe.
