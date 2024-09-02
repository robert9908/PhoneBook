using Microsoft.Extensions.Options;
using PhoneBook.API.Data;
using Microsoft.EntityFrameworkCore;
using PhoneBook.API.Repositories;
using PhoneBook.API.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PhoneBookDbContext>(option =>
option.UseSqlServer(builder.Configuration.GetConnectionString("PhoneBookConnectionString")));

builder.Services.AddScoped<IContactRepository, SQLContactRepository>();

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
