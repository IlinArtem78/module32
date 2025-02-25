using Microsoft.AspNetCore.Hosting;


class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRouting(); // Добавляем сервисы маршрутизации
        // Настраиваем приложение
        var app = builder.Build();
        Configure(app, app.Environment); // Вызываем метод Configure
        app.Run();
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Проверяем, не запущен ли проект в среде разработки
        if (env.IsDevelopment())
        {
            // 1. Добавляем компонент, отвечающий за диагностику ошибок
            app.UseDeveloperExceptionPage();
        }

        // 2. Добавляем компонент, отвечающий за маршрутизацию
        app.UseRouting();
        //3. Добавляем компонент для логирования запросов с использованием метода Use
        app.Use(async (context, next) =>
        {
            // Для логирования данных о запросе используем свойства объекта HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
            await next.Invoke();
        });

        // 4. Добавляем компонент с настройкой маршрутов
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync($"Welcome to the: {env.ApplicationName}!");
            });
        });
        //5.Страницы 
        app.Map("/about", appBuilder =>
        {
            appBuilder.Run(async context => await context.Response.WriteAsync($"{env.ApplicationName} - ASP.Net Core tutorial project"));
        });
        app.Map("/config", appBuilder =>
        {
            appBuilder.Run(async context => await context.Response.WriteAsync($"App name: {env.ApplicationName}. App running configuration: {env.EnvironmentName}"));
        });



        // Завершим вызовом метода Run
        app.Run(async (context) =>
        {
            await context.Response.WriteAsync($"Page not found!");
        });


    }
  







}




    





