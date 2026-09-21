# 5.   Create one REST API module

Date: 2026-09-21

## Status

Accepted

## Context

We need to expose the API of our application to the outside world. 

## Possible solutions
1. Create one .NET Core MVC host application which contains all endpoints. This host application will have references to all business modules and communicates with them directly:</br>

Host/API references:</br>
Transactions module</br>
Identity module</br>
Budget module</br>
Categorization module</br>

## Decision

Creating separate API projects for each module will add complexity and little value. Grouping endpoints for a particular business module in a special directory is enough. Another layer on top of the module is unnecessary.

## Consequences
- We will have only one API layer/module
- Each controller has responsibility to delegate Command/Query processing to appropriate module
- We don't need to scan other projects than host for controllers, routes and other MVC mechanisms
- API configuration is easier
- Overall complexity of API layer is lower
- Complexity of each controller is a little bit higher
