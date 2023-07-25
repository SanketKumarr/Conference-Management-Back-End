using Conference_Management_Web_Application.Business;
using Conference_Management_Web_Application.Business.Interfaces;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adding the Controllers for handling HTTP requests.
builder.Services.AddControllers();

// Adding the API Explorer for API endpoint exploration.
builder.Services.AddEndpointsApiExplorer();

// Adding Swagger for generating API documentation.
builder.Services.AddSwaggerGen();

// Adding MediatR for handling Mediator pattern in the application.
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Registering the Business Logic Layer (BLL) as a Scoped service.
builder.Services.AddScoped<IBLL, BLL>();

// Adding Cross-Origin Resource Sharing (CORS) support to allow requests from any origin.
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

// If the application is running in Development mode, enable Swagger and Swagger UI.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS middleware to handle cross-origin requests.
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Redirect HTTP requests to HTTPS.
app.UseHttpsRedirection();

// Enable authorization middleware for handling authorization-related tasks.
app.UseAuthorization();

// Map the controllers defined in the application.
app.MapControllers();

// Run the application.
app.Run();
