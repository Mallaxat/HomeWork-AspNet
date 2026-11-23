namespace HomeWork.Model;

public class ReportMiddleware
{
    //Передаем делегат для некста
    private readonly RequestDelegate _next;
    //Конструктор должен принять след цепочку 
    public ReportMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    //Asp будет искать метод именно с таким названием для вызова
    public async Task InvokeAsync(HttpContext context, IServiceProvider services)
    {
        var rep = context.RequestServices.GetRequiredKeyedService<IReportService>("Rel2");
        //чтобы кодировка не сбилась
        context.Response.ContentType = "text/plain; charset=utf-8";
        await context.Response.WriteAsync($"Релиз через медивер: {rep.PrintReport()}");

        //Передаем контекст в следующий обработчик 
        await _next(context);
    }
    
}