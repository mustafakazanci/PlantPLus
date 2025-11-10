using Microsoft.EntityFrameworkCore;
using PlantDiseaseApi.Data;
using PlantDiseaseApi.Services;

var builder = WebApplication.CreateBuilder(args);

// --- Servis Kayıtları ---
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMLService, MLService>();

var app = builder.Build();

// --- UYGULAMA BAŞLANGIÇ GÖREVLERİ ---
// Bu bloğu, veritabanını oluşturmak ve başlangıç verilerini eklemek için kullanıyoruz.
ApplyMigrationsAndSeedData(app);

// --- Uygulama Yapılandırması (Middleware) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();


// --- YARDIMCI METOT ---
// Bu metot, veritabanı migrate edildikten sonra SeedData'yı çalıştırır.
void ApplyMigrationsAndSeedData(IHost app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Veritabanı migration'ları kontrol ediliyor...");
            context.Database.Migrate(); // Bu, 'dotnet ef database update' ile aynı işi yapar.
            logger.LogInformation("Migration'lar başarıyla uygulandı.");
            
            logger.LogInformation("SeedData kontrol ediliyor...");
            SeedData.Initialize(services);
            logger.LogInformation("SeedData başarıyla çalıştırıldı.");
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Uygulama başlangıcında veritabanı işlemleri sırasında bir hata oluştu.");
        }
    }
}