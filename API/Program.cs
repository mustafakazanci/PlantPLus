using Microsoft.EntityFrameworkCore;
using PlantDiseaseApi.Data;
using PlantDiseaseApi.Services; // Bu using'i EF Core için ekliyoruz

var builder = WebApplication.CreateBuilder(args);

// --- Servis Kayıtları ---
// API Controller'larını kullanabilmemiz için Controller servisini ekliyoruz
builder.Services.AddControllers();

builder.Services.AddScoped<IMLService, MLService>();

// Swagger/OpenAPI dokümantasyonu için gerekli servisleri ekliyoruz
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- Uygulama Yapılandırması (Middleware) ---
var app = builder.Build();

// Geliştirme ortamında Swagger UI'ı etkinleştiriyoruz.
// Eğer üretimde de kullanmak istersek 'if (app.Environment.IsDevelopment())' bloğunun dışına taşıyabiliriz.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // HTTPS yönlendirmesini etkinleştirir

app.UseAuthorization(); // Yetkilendirme middleware'ini ekler (eğer kullanırsak)

app.MapControllers(); // Controller'lardaki endpoint'leri HTTP isteklerine bağlar


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Veritabanı seed edilirken bir hata oluştu.");
    }
}


app.Run();