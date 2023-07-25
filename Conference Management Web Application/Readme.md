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
		- SignUpCommand
		- LoginCommand
		
	- Queries {IRequest}
		
	- Handlers {IRequestHandler}
		- SignUpCommandHandler
		- LoginCommandHandler

- Business
	- Interfaces
		- IBLL.cs
	- BLL.cs
