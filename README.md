# CandidateHub API

## Overview

CandidateHub is a RESTful API designed to manage candidate information. It allows users to add, update, and retrieve candidate data efficiently. The API uses a clean architecture that separates concerns across different layers, enabling easy maintainability and scalability.

## Features

- **REST API** for managing candidate data
- **Entity Framework Core** for data access
- **Memory Caching** for optimized performance
- **Automated Database Migration**
- **Unit Tests** for core functionalities

## Architecture

The project follows a multi-layered architecture to ensure separation of concerns:

- **API Layer**: Exposes RESTful endpoints for the application.
- **Application Layer**: Contains business logic and services.
- **Domain Layer**: Includes domain entities and interfaces.
- **Infrastructure Layer**: Handles data persistence and repository implementations.
- **Tests Layer**: Contains unit tests to validate the core functionalities of the application.

## Technologies Used

- **.NET **: The framework for building the API.
- **Entity Framework Core**: ORM for database access.
- **MemoryCache**: In-memory caching to enhance performance.
- **AutoMapper**: To map between different object models.
- **Moq**: Mocking framework for unit testing.
- **xUnit**: Testing framework for unit tests.
- **Swagger**: API documentation and testing interface.

### API Endpoints

- **Get all candidates**: `GET /api/candidates`
- **Get candidate by ID**: `GET /api/candidates/{id}`
- **Add or update a candidate**: `POST /api/candidates`
- **Delete a candidate**: `DELETE /api/candidates/{id}`

### Caching

The API uses in-memory caching to store and quickly retrieve candidate data. Cached data is automatically invalidated whenever a candidate is added, updated, or deleted to ensure the data remains consistent and up-to-date.

## Assumptions

1. **Email as Unique Identifier**: Each candidate's email is unique and serves as the primary identifier for adding or updating their information.
2. **In-memory Caching**: Assumes the application is running in an environment where in-memory caching is sufficient.
3. **Self-deploying**: The application is designed to be self-deploying. Opening the solution and running it in Visual Studio should set up the database and run the application without additional configuration.

## Improvements

1. **Database Flexibility**: Implementing repository and unit of work patterns allows for easy swapping of the database backend in the future.
2. **Enhanced Caching**: Introduce distributed caching solutions like Redis for scalability in multi-server deployments.
3. **Advanced Logging**: Integrate a logging framework to provide better insights and debugging capabilities.
4. **Error Handling**: Implement a global error handling mechanism to provide consistent and meaningful error responses.
5. **Security**: Add authentication and authorization mechanisms to secure the API endpoints.
6. **Deployment**: Consider Dockerizing the application for easier deployment and scaling in various environments.
