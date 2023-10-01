using System.Reflection;
using ChromeExtension.Services.Implementations;
using ChromeExtension.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMemoryService, MemoryRepository>();

builder.Services.AddSwaggerGen( m => {
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    m.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChromeExtension API");
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
