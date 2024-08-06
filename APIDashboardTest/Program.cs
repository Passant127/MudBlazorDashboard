using Dashboard.Application.Contracts;
using Dashboard.Application.Mapping;
using Dashboard.Application.Services;
using Dashboard.Infrastrcuture.BaseContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<DashboardDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Dashboard.Infrastrcuture")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
                  
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddAutoMapper ( typeof(MappingProfile) );
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
