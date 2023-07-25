- Domain
	- Model
		- User.cs

- Application
	- LoginController
        * SignUp
        * Login
    
    - AdminController
        * Delete
        * Update
        * Get
	
- Infrastructure
	
	- Commands {IRequest}
		- AddUserCommand.cs
		- UpdateUserCommand.cs
		- DeleteUserCommand.cs
		
	- Queries {IRequest}
		- GetAllUsersQuery.cs
		- GetUserByIdQuery.cs
		
	- Handlers {IRequestHandler}
		- AddUserCommandHandler.cs
		- UpdateUserCommandHandler.cs
		- DeleteUserCommandHandler.cs 
		- GetUsersQueryHandler.cs
		- GetUserByIdQueryHandler.cs

- Business
	- Interfaces
		- IBLL.cs
	- BLL.cs
	
```
# MediatR.Extensions.Microsoft.DependencyInjection

# System.Data.SqlClient 

# Dapper
```

# In Program
```
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddCors();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
```