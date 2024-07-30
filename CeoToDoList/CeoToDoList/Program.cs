using CeoToDoList.Data;
using CeoToDoList.Repositories;
using Microsoft.EntityFrameworkCore;
using CeoToDoList.Mappings;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CEO List API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);

                    var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
                    xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));
                });

// Connecting to the database
builder.Services.AddDbContext<CeoDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CeoConnectionString")));

// repo inject
builder.Services.AddScoped<IListRepositories, SQLListRepository>();
builder.Services.AddScoped<ITaskRepositories, SQLTaskRepository>();

// mapper inject
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();