orders-spike
====
This repo hosts a proof-of-concept .NET Core Microservice that handles customers for the fictitious company ACME Corporation.

## Technologies
* .NET Core 3.1 / ASP .NET Core 3.1
* Entity Framework Core
* FluentAssertions
* MediatR
* RestSharp
* Serilog


Overview
-------------

The solution is broken down into the following projects:
| Acme.Orders|
| ------------- |
| Acme.Orders.Api |
| Acme.Orders.Api.AcceptanceTests |
| Acme.Orders.Application |
| Acme.Orders.Common |
| Acme.Orders.Domain |
| Acme.Orders.Domain.UnitTests |
| Acme.Orders.Infrastrucutre |
| Acme.Orders.TestSdk |

TODO
------
* Add Business Exception handling
* Add MediatR validation
* Add Acceptance Test/Unit Test projects
* Add Messaging (Mass Transit/RabbitMQ)



