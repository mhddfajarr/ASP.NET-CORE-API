using Latihan.Context;
using Latihan.Repositories;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LatihanSelasa")));
builder.Services.AddScoped<UniversityRepository>();
builder.Services.AddScoped<AccountRepository>();
builder.Services.AddScoped<GetAllDataRepository>();

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

//builder.Services.AddControllers()S
//    .AddJsonOptions(opt =>
//    {
//        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//        opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//    });

builder.Services.AddCors(e => {
    e.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.Run();
