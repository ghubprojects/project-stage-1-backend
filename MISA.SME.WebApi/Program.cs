using Microsoft.AspNetCore.Mvc;
using MISA.SME.Application;
using MISA.SME.Domain;
using MISA.SME.Infrastructure;
using MISA.SME.WebApi;
using MySqlConnector;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddDomain()
        .AddApplication()
        .AddInfrastructure();

    // Config database connection
    builder.Services.AddScoped<IDbConnection>((sp) => new MySqlConnection(builder.Configuration.GetConnectionString("Mysql")));

    builder.Services.AddScoped<IDbTransaction>((sp) => new MySqlConnection(builder.Configuration.GetConnectionString("Mysql")).BeginTransaction());

    builder.Services.AddControllers().AddJsonOptions((options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; }));

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add cors
    builder.Services.AddCors();

    // Add SuppressModelStateInvalidFilter
    builder.Services.Configure<ApiBehaviorOptions>(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseMiddleware<ExceptionMiddleware>();

    // Enable cors
    app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());

    app.Run();
}

