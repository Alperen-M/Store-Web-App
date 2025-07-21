using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApplication1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// Swagger Ayarları
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "🛍️ Store & Product API",
        Version = "1.0",  // "v1" yerine "1.0" veya "3.0.0" yaz
        Description = "📘 Mağaza ve ürün yönetimi için RESTful API dokümantasyonu.\n\n🔐 Herkese açık (authentication gerekmez).",
        Contact = new OpenApiContact
        {
            Name = "Alperen Mengünoğul",
            Email = "mengunogulalperen@gmail.com",
            Url = new Uri("https://github.com/Alperen-M")
        },
        License = new OpenApiLicense
        {
            Name = "MIT Lisansı",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});

var app = builder.Build();

// HTTP pipeline yapılandırması
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Swagger UI Özelleştirme
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "🛍️ Store & Product API v1");
        c.DocumentTitle = "Store & Product API Docs";
        c.InjectStylesheet("/wwwroot/swagger-ui/custom.css"); // tema dosyası
        c.InjectJavascript("/wwwroot/swagger-ui/custom.js");  // ek açıklama
        c.RoutePrefix = "swagger"; // Burası boş değil, "swagger" olmalı
    });
}

// wwwroot klasörünü kullanmak için gerekli
app.UseStaticFiles(); // custom.css ve custom.js dosyaları için gerekli

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
