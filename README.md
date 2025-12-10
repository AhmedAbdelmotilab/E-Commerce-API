# E-Commerce Web API

The E-Commerce Web API is a backend The E-Commerce Web API is a backend solution built in C# for managing products, orders, and users. It follows a clean, modular architecture separating domain, persistence, services, and API layers.
The API provides scalable endpoints for integrating e-commerce functionalities into web applications.
---

## Solution Structure

- **E-Commerce.Domain**  
  Contains domain models and business logic. This is where core entities representing the e-commerce system (such as products, orders, users, etc.) and their behaviors are defined.

- **E-Commerce.Persistence**  
  Implements data-access logic, repositories, and database configuration. This layer handles interaction with the underlying database, providing the implementation for storing and retrieving domain objects.

- **E-Commerce.Presentation**  
  Responsible for presenting the API endpoints and handling HTTP requests and responses. This project typically contains controllers, routing, and input validation logic to expose API services to clients.

- **E-Commerce.Services Abstraction**  
  Defines service interfaces and application service contracts used across the solution. This enables loose coupling between the service consumers and their implementations.

- **E-Commerce.Services**  
  Contains concrete implementations of the service abstractions, encapsulating business operations that span across multiple domain objects.

- **E-Commerce.Shared**  
  Houses shared utilities, constants, and helper classes that are used by multiple projects within the solution.

- **E-Commerce.Web**  
  Hosts the Web API application, bootstrapping all layers and configuring middleware. This is the entry point for the solution and where the actual API runs.

---

## Getting Started

> **Requirements**:  
> - [.NET Core SDK](https://dotnet.microsoft.com/download)
> - Any IDE supporting C# (.NET), e.g., [Visual Studio](https://visualstudio.microsoft.com/) or [Rider](https://www.jetbrains.com/rider/)

> **Restoring & Building**  
> 1. Clone this repository  
>    `git clone https://github.com/AhmedAbdelmotilab/E-Commerce-API.git`
> 2. Restore NuGet packages  
>    `dotnet restore`
> 3. Build the solution  
>    `dotnet build`

> **Running the API**  
> 1. Navigate to the `E-Commerce.Web` project folder  
> 2. Run  
>    `dotnet run`

---

## Included Files

- `.gitignore` — Configures files and folders to exclude from version control.
- `global.json` — Specifies the .NET SDK version for the solution.
- `E-CommerceSolution.sln` — Visual Studio solution file, containing all projects.
- `E-CommerceSolution.sln.DotSettings.user` — User-specific IDE settings.

---

## Project Structure (Folders)

- `.idea/` — IDE-specific configuration.
- `E-Commerce.Domain/`
- `E-Commerce.Persistence/`
- `E-Commerce.Presentation/`
- `E-Commerce.Services Abstraction/`
- `E-Commerce.Services/`
- `E-Commerce.Shared/`
- `E-Commerce.Web/`

---

## Technologies & Language

- **C# (.NET Core)** – All solution components are implemented in C#.

---

## Repository Purpose

This solution forms the backend foundation for an e-commerce platform, focusing on modularity and clean separation of concerns. It provides core APIs for e-commerce operations, following best practices for maintainability and scalability.

---

## License

Please check the repository for licensing details.
