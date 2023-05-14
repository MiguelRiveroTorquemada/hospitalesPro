using Microsoft.OpenApi.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Data;
using Clases;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "hospitalesDB",
        Description = "Hospitales miguel ",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


builder.Services.AddDbContext<DataContext>(opt =>
{
    if (builder.Environment.IsDevelopment())
    {
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        opt.UseSqlServer(connectionString);
    }
    else
    {
        opt.UseInMemoryDatabase("DataContext");
    }
});

builder.Services.AddCors(options =>

//cors
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
                      policy  =>
                      {
                        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                        
                      });
});
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("MyAllowSpecificOrigins");
// Configure the HTTP request pipeline.
//if (apps.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
//}

app.MapControllers();

app.Run();