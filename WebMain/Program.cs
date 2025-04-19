using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpcClient<Contracts.UserServiceRpc.UserServiceRpcClient>(options =>
{
    options.Address = new Uri("http://userservice:8080"); // имя контейнера!
    //options.Address = new Uri("http://localhost:5001");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    //return new SocketsHttpHandler
    //{
    //    EnableMultipleHttp2Connections = true
    //};
});

builder.Services.AddGrpcClient<Contracts.ProductServiceRpc.ProductServiceRpcClient>(options =>
{
    options.Address = new Uri("http://productservice:8080"); // имя контейнера в docker-compose
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
