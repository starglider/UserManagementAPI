using Microsoft.AspNetCore.Http;
using UserManagementAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register UserService
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); 
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add global exception handling middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("{\"error\"}: {\"Internal server error: "+ex.Message+"\"}");
    }
});

// Add request logging middleware
app.UseMiddleware<UserManagementAPI.Middleware.RequestLoggingMiddleware>();

// Map UserController endpoints
app.MapControllers();

app.Run();


