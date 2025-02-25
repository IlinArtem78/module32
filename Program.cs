using Microsoft.AspNetCore.Hosting;


class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddRouting(); // ��������� ������� �������������
        // ����������� ����������
        var app = builder.Build();
        Configure(app, app.Environment); // �������� ����� Configure
        app.Run();
    }

    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // ���������, �� ������� �� ������ � ����� ����������
        if (env.IsDevelopment())
        {
            // 1. ��������� ���������, ���������� �� ����������� ������
            app.UseDeveloperExceptionPage();
        }

        // 2. ��������� ���������, ���������� �� �������������
        app.UseRouting();
        //3. ��������� ��������� ��� ����������� �������� � �������������� ������ Use
        app.Use(async (context, next) =>
        {
            // ��� ����������� ������ � ������� ���������� �������� ������� HttpContext
            Console.WriteLine($"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
            await next.Invoke();
        });

        // 4. ��������� ��������� � ���������� ���������
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync($"Welcome to the: {env.ApplicationName}!");
            });
        });
        //5.�������� 
        app.Map("/about", appBuilder =>
        {
            appBuilder.Run(async context => await context.Response.WriteAsync($"{env.ApplicationName} - ASP.Net Core tutorial project"));
        });
        app.Map("/config", appBuilder =>
        {
            appBuilder.Run(async context => await context.Response.WriteAsync($"App name: {env.ApplicationName}. App running configuration: {env.EnvironmentName}"));
        });



        // �������� ������� ������ Run
        app.Run(async (context) =>
        {
            await context.Response.WriteAsync($"Page not found!");
        });


    }
  







}




    





