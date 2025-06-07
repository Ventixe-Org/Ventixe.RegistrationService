using Microsoft.EntityFrameworkCore;
using Ventixe.RegistrationService.Data;
using Ventixe.RegistrationService.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    if (!db.Registrations.Any())
    {
        db.Registrations.AddRange(
            new Registration { EventId = 1, Name = "Alice Andersson", Email = "alice@example.com", Created = DateTime.UtcNow },
            new Registration { EventId = 2, Name = "Bob Berg", Email = "bob@example.com", Created = DateTime.UtcNow.AddMinutes(-10) }
        );
        db.SaveChanges();
    }
}

app.MapControllers();
app.Run();