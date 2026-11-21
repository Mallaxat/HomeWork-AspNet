using Microsoft.AspNetCore.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Map("/use", branch =>
{
    branch.Use(async (context, next) =>
    {
        Console.WriteLine("Первый middleware: начало обработки");
        await next(context);
        Console.WriteLine("Первый middleware: после next");
    });

    branch.Use(async (context, next) =>
    {
        Console.WriteLine("HeaderMiddleware: добавление заголовка");
        context.Response.Headers["X-Processed-By"] = "HeaderMiddleware";
        await next(context);
        Console.WriteLine("HeaderMiddleware: после next");
    });

    branch.Run(context =>
    {
        Console.WriteLine("Use: формирование ответа");
        context.Response.ContentType = "text/plain; charset=utf-8";
        return context.Response.WriteAsync("Запрос обработан.");
    });
});

app.Map("/middleware", branch =>
{
    branch.UseMiddleware<Middleware>();
    branch.Run(context => Results.Text("OK").ExecuteAsync(context));
});

app.Run(context =>
{
    context.Response.ContentType = "text/plain; charset=utf-8";
    return context.Response.WriteAsync(
        $"URL: {context.Request.GetDisplayUrl()}\nМетод: {context.Request.Method}\nВремя сервера: {DateTimeOffset.Now:O}");
});

app.Run(context =>
{
    Console.WriteLine("Этот обработчик не должен выполняться.");
    return context.Response.WriteAsync("Недостижимый ответ.");
});

app.Run();
