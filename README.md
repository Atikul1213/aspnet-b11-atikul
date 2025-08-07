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

# Features
- User Management- Role based user creation and management with claim based authorization
- Customer Management- Track and manage customer information
- Product Management- Complete product catalog with image upload and compression and generate the product barcode and unique product sku
- Sales Management- Process orders and generate invoices for customers
- Balance Transfer- Track and manage financial transaction between account


# Worker Service
-  A background service build with .NET Worker Service that handles:
- Get the command from the AWS SQS command
- AWS S3 Uploads (Take the image from the local storage and upload the image into the S3 bucket as per the SQS command)

# Testing
- NUnit
- Shouldly
- Moq
- Assembly, Act, Assert
