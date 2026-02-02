using ImageOrganizer.Api.Services;
using ImageOrganizer.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});



// Register SQL Server DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


// Dependency Injection
builder.Services.AddScoped<ImageService>();


var app = builder.Build();

// Enable Swagger (works in .NET 8)
app.UseSwagger();
app.UseSwaggerUI();

// Middleware
app.UseHttpsRedirection();

app.UseCors("AllowFrontend");
// Serve static files from the "Uploads" folder
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")
    ),
    RequestPath = "/uploads"
});


app.MapControllers();

app.Run();
