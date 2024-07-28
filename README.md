# TechBlogServices Documentation

## Overview
TechBlogServices is a blog platform built using a range of modern software design principles and patterns. The service leverages minimal API, Carter, CQRS MediatR pattern, Clean Architecture, Vertical Slice Architecture, Domain-Driven Design (DDD), Abstract Validation, Custom Exception Handling, Entity Framework Core, Authentication and Authorization, and xUnit for testing.

## Key Technologies and Patterns
- **Minimal API**: Used for creating lightweight and efficient APIs.
- **Carter**: A library that simplifies the creation of HTTP-based APIs in .NET.
- **CQRS MediatR Pattern**: Separates read and write operations to enhance scalability and maintainability.
- **Clean Architecture**: Ensures the system is modular, flexible, and easy to maintain.
- **Vertical Slice Architecture**: Organizes the codebase by features instead of layers, promoting high cohesion.
- **Domain-Driven Design (DDD)**: Focuses on the core domain logic, ensuring that the business rules are clearly defined and encapsulated.
- **Abstract Validation**: Centralized validation logic for ensuring data integrity.
- **Custom Exception Handling**: Standardized error handling across the application.
- **Entity Framework Core**: An ORM for data access, providing an abstraction over database interactions.
- **Authentication and Authorization**: Secure API endpoints using JWT tokens.
- **xUnit**: A testing framework for ensuring code quality and reliability through automated tests.

## Architecture Overview
TechBlogServices is structured following the principles of Clean Architecture, Vertical Slice Architecture, and DDD. This ensures a clear separation of concerns, making the system easier to develop, test, and maintain.

### Domain Layer
- Contains the core business logic and domain entities.
- Value Objects and Aggregates as needed.

### Application Layer
- Contains the application logic and handles use cases.
- Uses CQRS MediatR pattern to separate command and query responsibilities.
- Defines commands, queries, and their respective handlers.

### Infrastructure Layer
- Contains the implementation details such as database access and external services.
- Configures Entity Framework Core for database interactions.
- Implements repositories for data access.
- Includes Abstract Validation and Custom Exception Handling.

### Presentation Layer
- Contains the minimal API endpoints.
- Uses Carter for routing and request handling.
- Secures APIs using JWT authentication.

## CQRS Pattern
CQRS (Command Query Responsibility Segregation) is a design pattern that separates the read and write operations of a system. This separation is beneficial for several reasons:
1. **Scalability**: Read and write workloads can be scaled independently.
2. **Security**: Different permissions can be applied to read and write operations.
3. **Performance**: Optimizations can be tailored specifically for read and write operations.

In TechBlogServices, CQRS is implemented using the MediatR library, which helps to decouple the application by allowing messages to be sent between different parts of the system without them needing to know about each other directly. This improves maintainability and testability.

## Vertical Slice Architecture
Vertical Slice Architecture is a design approach where the system is organized by features rather than by technical layers (such as controllers, services, repositories). Each feature is developed as a vertical slice, containing all the layers from the database to the user interface that are required to implement that feature.

### Benefits
1. **High Cohesion**: Each feature is self-contained and includes everything necessary to fulfill a specific user requirement.
2. **Low Coupling**: Changes in one feature are less likely to impact other features, making the system more maintainable.
3. **Focused Development**: Developers can work on individual features without needing to understand the entire system.

In TechBlogServices, each vertical slice includes:
- **Command Handlers**: For processing commands related to the feature.
- **Query Handlers**: For retrieving data related to the feature.	
- **Validation and Exception Handling**: Specific to the feature.

## API Endpoints

### User APIs
1. **Register**: `POST /register/blogger`
2. **Login**: `POST /users/login`

### Article APIs
1. **Create Article**: `POST /articles`
2. **Update Article**: `PUT /articles`
3. **Delete Article**: `DELETE /articles/{id}`
4. **Get Articles**: `GET /articles`
5. **Get Article by Id**: `GET /articles/{articleId}`
6. **Get Articles by Category**: `GET /articles/category/{categoryId}`
7. **Get Articles by User**: `GET /articles/user/{userId}`

### Category APIs
1. **Create Category**: `POST /categories` (Admin only)
1. **Update Category**: `PUT /categories` (Admin only)
1. **Delete Category**: `DELETE /categories` (Admin only)
2. **Get Categories**: `GET /categories`

## Clean Code Practices
1. **Unit Testing**: Ensure at least 80% code coverage using xUnit.
2. **Authentication and Authorization**: Implement JWT for securing APIs.
3. **Design Patterns**: Use Repository and Unit of Work patterns.
4. **SOLID Principles**: Ensure each class follows single responsibility, open/closed, Liskov substitution, interface segregation, and dependency inversion principles.

## Integration Testing with a Separate Relational Test Database

### Overview
Integration tests verify that different components of the application work together as expected. Using a separate relational test database provides a more realistic environment compared to an in-memory database. This approach ensures that your tests accurately reflect interactions with a real database system.

### Best Practices for Integration Testing with a Relational Database

1. **Use a Dedicated Test Database**: Ensure the test database is isolated from production and development databases to avoid data contamination.
2. **Configure Test Environment**: Use environment-specific configuration files to manage connection strings and other settings for testing.
3. **Clean Up Test Data**: Implement robust strategies to clean up data after tests to ensure consistent test results and prevent data buildup.
4. **Test Coverage**: Aim for comprehensive test coverage, including edge cases and error conditions, to ensure the reliability of the system.

Integration tests with a separate relational test database provide a thorough validation of your application’s interactions with a real database, ensuring that the system operates correctly and reliably in a production-like environment.

## Multiple Environment Support

TechBlogServices supports multiple environments, including local IIS and Docker environments, to ensure flexibility and consistency across different deployment scenarios.

### Local Development
1. **Configuration**: Use `appsettings.json` and `appsettings.Test.json` for local development settings. This file includes configurations specific to the development environment, such as local database connection strings and development-specific feature toggles.
2. **Environment Variables**: Set environment variables in your local environment to override configuration settings as needed. This helps in managing settings without altering the codebase.

### Docker Environment
1. **Docker Configuration**: Use Docker to containerize the application and its dependencies. Docker provides a consistent environment for development, testing, and production.
2. **Docker Compose**: Define services and their configurations in a `docker-compose.yml` file. This file includes definitions for the application container, database container, and any other dependencies.
3. **Environment Variables**: Configure environment variables in the `docker-compose.override.yml` file to manage different settings for the Docker environment.

### Managing Multiple Environments
1. **Environment-Specific Configuration Files**: Use environment-specific configuration files (e.g., `appsettings.json`, `appsettings.Test.json`) to manage settings for different environments.
2. **Different Local Servers**: Use 'launchSettings.json' to configure different server on your local environment.

By supporting multiple environments, TechBlogServices ensures that the application can be developed, tested, and deployed consistently across various scenarios, including local development and Docker-based environments.