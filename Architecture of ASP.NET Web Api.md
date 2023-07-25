- Domain
	- Model
		- Employee.cs
        
- Application
	- EmployeeController.cs
	
- Infrastructure
	
	- Commands {IRequest}
		- AddEmployeeCommand.cs
		- UpdateEmployeeCommand.cs
		- DeleteEmployeeCommand.cs
		
	- Queries {IRequest}
		- GetAllEmployeesQuery.cs
		- GetEmployeeByIdQuery.cs
		
	- Handlers {IRequestHandler}
		- AddEmployeeCommandHandler.cs
		- UpdateEmployeeCommandHandler.cs
		- DeleteEmployeeCommandHandler.cs 
		- GetEmployeesQueryHandler.cs
		- GetEmployeeByIdQueryHandler.cs

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