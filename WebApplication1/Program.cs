using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebApplication1.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// FluentValidation + Controller servisleri
builder.Services.AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<Program>(); // Validators klasöründeki tüm validatörleri tarar
        config.DisableDataAnnotationsValidation = true; // [Required] gibi annotation'ları devre dışı bırakır
    });

// Dependency Injection (Service & Repository)
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();

// Swagger yapılandırması
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

    // XML yorumları dahil et (Swagger'da açıklamalar gösterilsin diye)
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "🛍️ Store & Product API v1");
    c.RoutePrefix = "swagger"; // Swagger UI yolu: http://localhost:{port}/swagger
});

// Middleware'ler
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
