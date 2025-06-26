using Eventflow.API.Endpoints;
using Eventflow.Infrastructure.Configurations;
using EventFlow.Application.Commands.RegisterUser;
using EventFlow.Application.Queries.Login;
using EventFlow.Domain.Event.User;
using MediatR.Registration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Fix for CS1503: Use the overload that accepts a configuration action instead of an assembly.  
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly));
//builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(typeof(UserRegisteredDomainEvent).Assembly));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(LoginUserQuery).Assembly));

// Add services to the container.  
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ServiceRegistrations.AddInfrastructure(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapAuthEndpoints();

app.Run();
