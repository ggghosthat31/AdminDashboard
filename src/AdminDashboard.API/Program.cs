using AdminDashboard.API.Middleware;
using AdminDashboard.API.Services;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

builder.Services.ConfigureDatabaseContext(config);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(config);
builder.Services.ConfigureApplication();
builder.Services.AddControllers();
builder.Services.ConfigureCors();
builder.Services.AddHttpClient();
builder.Services.AddMvc();
builder.Services.AddOpenApi();
builder.Services.ConfigureSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowWebApp");
app.UseRouting();
app.UseHttpsRedirection();
app.UseMiddleware<RequestBrokerMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();