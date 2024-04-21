# FlightApp Api 

## Requirements
 * .NET 8.0
## Architecture overview

Clean Architecture is a software design approach that prioritizes separation of concerns and maintainability. It emphasizes structuring applications in a way that keeps the core business logic independent of external concerns like frameworks or databases. At its heart lies the principle of dependency inversion, ensuring that high-level policies aren't dependent on low-level details. This ensures adaptability and testability, making it easier to evolve and maintain software systems over time.
![image](https://github.com/bartpob/FlightApp/assets/63394798/81dcfc03-2d1d-4d13-af95-999c225e3a12)

## Entity Relationship Diagram

![image](https://github.com/bartpob/FlightApp/assets/63394798/7f3249b3-0891-4208-9347-792d8358b49e)

## Api Endpoints

Swagger provides documentation for endpoints. Access to the documentation is available when running the application through Visual Studio.

## Authentication and Authorization

The authorization is handled by the AuthController. It provides an endpoint for obtaining a bearer token. I used JwtToken as the authorization mechanism. For user identification, I utilized IdentityDbContext from EntityFrameworkCore.


## Data Validation

I used the FluentValidation package for data validation, ensuring that input data adheres to the specified business logic requirements.


## Persistence

To showcase my familiarity with EntityFramework, I used an InMemoryDatabase. This allowed me to avoid the need for creating a SQL server. The initial values of dictionaries and user account are hard-coded in the Program.cs file.

## CQRS pattern


The CQRS pattern separates read (queries) from write (commands) operations. Commands mutate the system state, while queries retrieve data. This separation enables optimization and scalability. I used the MediatR package for implementation that pattern.

## Unit Testing

I ensured tests for both the application and infrastructure layers. In the application layer, each command, query, and validator has multiple tests. In the infrastructure layer, I provided tests for logging mechanisms and repositories.
I used Moq library to mocking required services in test scenarios.
