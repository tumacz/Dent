using TheApp.Infrastructure.Extensions;
using TheApp.Infrastructure.Seeders;
using TheApp.Application.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Rejestrowanie kontrolerów i usług
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

// 🔹 CORS dla React frontend (localhost:5173)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 🔹 Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TheApp API",
        Version = "v1",
        Description = "API for Dental Studio management"
    });
});

var app = builder.Build();

// 🔹 Seedowanie danych startowych
using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DentalStudioSeeder>();
    var admin = scope.ServiceProvider.GetRequiredService<AdminSeeder>();

    await seeder.Seed();
    await admin.Seed();
}

// 🔹 Konfiguracja middleware
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 🔹 Użyj CORS zanim autoryzacja
app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();

// 🔹 Swagger UI dostępne pod "/"
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "TheApp API v1");
    options.RoutePrefix = ""; // <-- Swagger pod rootem
});

// 🔹 Wymagane do testów integracyjnych


app.Run();

public partial class Program { }