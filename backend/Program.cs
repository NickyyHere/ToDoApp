using Microsoft.EntityFrameworkCore;
using ToDoApp.repository.dbcontext;
using ToDoApp.repository.interfaces;
using ToDoApp.repository;
using ToDoApp.services.interfaces.ITodoService;
using ToDoApp.services;
using System.Text.Json.Serialization;
using ToDoApp.services.interfaces;
using ToDoApp.factory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

AppContext.SetSwitch("Nphsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<TodoItemFactory>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITechnologyService, TechnologyService>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITechnologyRepository, TechnologyRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
