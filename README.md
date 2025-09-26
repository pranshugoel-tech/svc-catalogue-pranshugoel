# Service Catalogue API

A **RESTful Web API** built with **ASP.NET Core 9.0** for managing and retrieving service catalogue data.

---

## 🚀 Getting Started

### Prerequisites

Ensure you have the following installed:

- [.NET SDK 9.0](https://dotnet.microsoft.com/download/dotnet/9.0)  
- [Docker](https://www.docker.com/get-started)  
- [Docker Compose](https://docs.docker.com/compose/install/)  

---

### Clone the Repository

```bash
git clone https://github.com/pranshugoel-tech/svc-catalogue-pranshugoel.git
cd svc-catalogue-pranshugoel
```
---
### Sample CSV File

A sample CSV file is included in the repository root for testing import functionality.
Ensure CSV columns match the ServiceCatalogueDto mapping:

```bash
Name,OwnerTeam,Tier,Lifecycle,Endpoints,Tags
User Management,Identity,gold,production,"https://api.example.com/users","auth;users"
Payments API,Finance,silver,preprod,"https://api.example.com/payments","payments;finance"
Inventory Service,Logistics,bronze,dev,"https://api.example.com/inventory","logistics;inventory"
```
---
### Makefile

Makefile is included in the repository root for testing, you can simplify commands:
To run locally - Set "DefaultConnection": "Data Source=Data/servicecatalogue.db" in appsettings.json
For Docker run - Set "DefaultConnection": "Data Source=/app/Data/servicecatalogue.db" in appsettings.json
```bash
make build     # Build the solution
make docker    # Build and run Docker container
```

---
### JWT Token Authentication

To access the protected endpoints, you need a JWT token.

#### Generate a JWT Token

Send a request to the authentication endpoint:
```bash
POST http://localhost:8080/api/Auth/login
```
Include your login credentials (e.g., username/password set to default admin/password) in the request body.

The response will include a JWT token.
#### Use the Token in Swagger

Open the Swagger UI for your API.
Click the Authorize button.

Enter the token in this format:

```bash
Bearer <JWT Token>
```
After authorizing, you can access the rest of endpoints.

---
## 🏗 Architecture Decision Records (ADR)

### ADR 001: Technology Stack

Framework: ASP.NET Core 9.0
Language: C# 11
Database: SQLite (for local dev / testing)
Containerization: Docker
API Documentation: Swagger/OpenAPI

### ADR 002: Swagger Integration
Swagger UI served at /swagger/index.html.
OpenAPI spec generated automatically by Swashbuckle.
Root (/) redirects to Swagger UI for easier access.
