using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApplication1.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controller servisi
builder.Services.AddControllers();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();


// Swagger yapılandırması (OpenAPI 3.0 uyumlu)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "🛍️ Store & Product API",
        Version = "v1",
        Description = "📘 Mağaza ve ürün yönetimi için RESTful API dokümantasyonu.",
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

    // XML yorum dosyasını kontrol ederek dahil et
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Swagger middleware (tüm ortamlar için aktif)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "🛍️ Store & Product API v1");
    c.RoutePrefix = "swagger"; // http://localhost:5257/swagger
});

// Diğer middleware'ler
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();