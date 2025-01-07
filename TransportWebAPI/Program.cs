using Microsoft.EntityFrameworkCore;
using TransportWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Получаем строку подключения из конфигурации
var connectionString = builder.Configuration.GetConnectionString("DBConnection")
    ?? throw new InvalidOperationException("Connection string 'DBConnection' not found.");

// Регистрируем DbContext для работы с базой данных
builder.Services.AddDbContext<TransportDbContext>(options =>
    options.UseSqlServer(connectionString));

// Добавляем сервисы для работы с контроллерами и представлениями
builder.Services.AddControllersWithViews(); // Для работы с контроллерами и представлениями

// Настройка Swagger с включением аннотаций
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations(); // Включаем аннотации Swagger
});


var app = builder.Build();

// Конфигурация HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Генерация Swagger JSON
    app.UseSwaggerUI(); // Интерфейс для взаимодействия с API через Swagger
}

// Разрешаем использование статических файлов
app.UseStaticFiles();

// Добавляем поддержку авторизации (если требуется)
app.UseAuthorization();


// Настройка маршрутов для статической HTML-страницы
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Schedule}/{action=index}/{id?}");
app.Run();
