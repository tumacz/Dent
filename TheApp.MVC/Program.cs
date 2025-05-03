using TheApp.Infrastructure.Extensions;
using TheApp.Infrastructure.Seeders;
using TheApp.Application.Extensions;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews(options =>options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);//Pass connection string

builder.Services.AddApplication();

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

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<DentalStudioSeeder>();
    var admin = scope.ServiceProvider.GetRequiredService<AdminSeeder>();

    await seeder.Seed();
    await admin.Seed();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=DentalStudio}/{action=Index}/{id?}");

//app.MapRazorPages();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "TheApp API v1");
    options.RoutePrefix = ""; // 👈 Swagger dostępny pod "/"
});

app.Run();

//tests require partial
public partial class Program { };