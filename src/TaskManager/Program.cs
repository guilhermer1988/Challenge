using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TaskManager.Infra.Data.Context;
using TaskManager.Infra.Interface;
using TaskManager.Infra.Repository;
using TaskManager.Service;
using TaskManager.Service.Interface;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddDbContext<ApplicationDbContext>();

/* Repositories */
services.AddScoped<IProjectRepository, ProjectRepository>();
services.AddScoped<ITaskRepository, TaskRepository>();
services.AddScoped<ITaskHistoryRepository, TaskHistoryRepository>();
services.AddScoped<ICommentRepository, CommentRepository>();

/* Services */
services.AddScoped<IProjectService, ProjectService>();
services.AddScoped<ITaskService, TaskService>();
services.AddScoped<IReportService, ReportService>();
services.AddScoped<ITaskHistoryService, TaskHistoryService>();
services.AddScoped<ICommentService, CommentService>();

services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses (e.g. Role)
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    // ignore omitted parameters on models to enable optional params (e.g. User update)
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole()
                  .AddDebug();
});

var app = builder.Build();

// migrate any database changes on startup (includes initial db creation)
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dataContext.Database.Migrate();
}

{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
