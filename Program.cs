using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

// Добавляем поддержку метрик
builder.Services.AddMetrics();

var app = builder.Build();

// Пример пользовательской метрики
var counter = Metrics.CreateCounter("my_requests_total", "Number of requests.");

app.UseRouting();

// Middleware для увеличения счётчика
app.Use(async (context, next) =>
{
    if (context.Request.Path != "/metrics")
    {
        counter.Inc();
    }
    await next();
});

// Регистрируем эндпоинт /metrics и главную страницу без UseEndpoints
app.MapMetrics(); // <-- этот метод добавляет эндпоинт /metrics
app.MapGet("/", () => "Тест ПСБ!");

app.Run();

app.Run();