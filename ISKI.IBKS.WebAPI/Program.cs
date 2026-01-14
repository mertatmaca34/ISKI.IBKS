using ISKI.IBKS.Infrastructure;
using ISKI.IBKS.Persistence.Contexts;
using ISKI.IBKS.WebAPI.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
var infraConfigDir = Path.Combine(AppContext.BaseDirectory, "Configuration");
if (!Directory.Exists(infraConfigDir))
{
    var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
    infraConfigDir = Path.Combine(projectRoot, "ISKI.IBKS.Presentation.WinForms", "Configuration");
}

if (Directory.Exists(infraConfigDir))
{
    builder.Configuration
        .AddJsonFile(Path.Combine(infraConfigDir, "sais.json"), optional: true, reloadOnChange: true)
        .AddJsonFile(Path.Combine(infraConfigDir, "plc.json"), optional: true, reloadOnChange: true);
}

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Basic Authentication
builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

builder.Services.AddAuthorization();

// Add Infrastructure services (PLC, SAIS API client, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

// Add DbContext
builder.Services.AddDbContext<IbksDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=ibks.db"));

var app = builder.Build();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IbksDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

