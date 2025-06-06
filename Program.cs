using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMetrics();

var app = builder.Build();


var counter = Metrics.CreateCounter("my_requests_total", "Number of requests.");

app.UseRouting();


app.Use(async (context, next) =>
{
    if (context.Request.Path != "/metrics")
    {
        counter.Inc();
    }
    await next();
});


app.MapMetrics(); 
app.MapGet("/", () => "Тест ПСБ!");

app.Run();

app.Run();