using Microsoft.EntityFrameworkCore;
using simpleWebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<UserContext>(opt =>
    opt.UseInMemoryDatabase("UserList"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// Set up default route to /api/Users
app.MapGet("/", context =>
{
    context.Response.Redirect("/api/User");
    return Task.CompletedTask;
});

app.Run();
