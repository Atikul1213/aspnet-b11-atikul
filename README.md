# Inventory Management System
A comprehensive Inventory Management System built with ASP.NET Core 9, following Clean Architecture principles with Domain-Driven Desing(DDD) and CQRS pattern.

# Architecture & Technologies
- Backed: ASP.NET Core MVC with Razor Pages
- Database: Microsoft SQL Server
- Architecture: Clean Architecture + DDD
- Design Pattern: CQRS ( Command Query Responsibility Segregation)
- Authentication & Authorization: ASP.NET Identity with claim based access control
- Containerization: Dockeried (Docker & Docker Componse)
- Cloud Integration: AWS S3 for image storage, AWS SQS for async processing
- Worker Service: User for background task to upload the image into by the worker service
- Testing: Unit tests using NUnit and Moq
