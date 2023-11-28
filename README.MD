# Product Management API

### Prerequisites

- .NET 6 SDK [Installation Guide](https://dotnet.microsoft.com/download/dotnet/6.0)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/GabrielGMarques/ProductManagementAPI.git
   cd ProductManagementAPI

2. Open the project on visual studio
 
3. Run migrations
   1. Set up the ProductManagement.Api as the startup project
   2. Open appsettings.json and put the ``ConnectionStrings.DefaultConnection`` key to the connection string of your local database
   3. Open the Package Management Console
      1. Set the ProjectManagement.Infra default project
      2. Run ```Update-Database``` to create the entities on the project



### Observations
1. Endpoints are protected by authentication
	1. To authenticate first create a user with the /auth/register endpoint 
	2. Then use the login endpoint to get the JWT Token (A improvment would return the jwt token on this endpoint together with the id of the user)
	3. Then authenticate with postman using ```bearer {{generated jwt token}}``
2. Category endpoints are only available to users with the role Admin (UserRole.Admin). So in order to test it, first create a user with the Admin role and authenticate with it)
3. If I have more time I would:
	1. Configure the appsettings for production and staging environments
	2. Add data validations on the controller
	3. Add Dtos for each request if needed (for example login and registry, removing the unused properties from the swagger and adding and adding required properties for each request)
	4. Standardize error codes and messages
	5. Create a BaseRepository and BaseService for improving reusability between services and between repositories
