using HomeWork.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();
//Регистрируем связку интерфейс + модель
builder.Services.AddSingleton<ISingID, SingID>();
builder.Services.AddScoped<IScopedId, ScopedID>();
builder.Services.AddTransient<ITransientID, TransientID>();

builder.Services.AddKeyedSingleton<IReportService, ReportRelise1>("Rel1");
builder.Services.AddKeyedSingleton<IReportService, ReportRelise2>("Rel2");

var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseAuthorization();

/*TASK_1
//context - по сути условие
//apiBranch-что делаем

app.UseWhen(
    context => context.Request.Path.StartsWithSegments("/api"),
apiBranch =>
{
    apiBranch.Use(async (context, next) =>
    {
        context.Response.ContentType = "text/plain";
        context.Response.Headers["X-BODY"] = "UseWhen";
        context.Response.WriteAsync("Headrs API");
        await next();
    });
});
app.MapWhen( context =>context.Request.Query["debug"]=="true",
    apiBranch =>
    {
        apiBranch.Run(context =>context.Response.WriteAsync("QUERY DEBUG TRUE"));
    });
app.Run(context=>context.Response.WriteAsync("\nHai basa"));
*/

/*Task_2
app.MapWhen(context => context.Request.Path.StartsWithSegments("/sing"),
    async (appBuilder) =>
    {
        appBuilder.Run(async (context) =>
        {
            var serv = context.RequestServices.GetRequiredService<ISingID>();
            await context.Response.WriteAsync($"Ex Id SINGLETON: {serv.GetID().ToString()}");
        });
    });
app.MapWhen(context => context.Request.Path.StartsWithSegments("/scoped"),
    async (appBuilder) =>
    {
        appBuilder.Run(async (context) =>
        {
            var serv = context.RequestServices.GetRequiredService<IScopedId>();
            await context.Response.WriteAsync($"Ex Id SCORED: {serv.GetID().ToString()}");
        });
    });
app.MapWhen(context => context.Request.Path.StartsWithSegments("/trans"),
    async (appBuilder) =>
    {
        appBuilder.Run(async (context) =>
        {
            var serv = context.RequestServices.GetRequiredService<ITransientID>();
            await context.Response.WriteAsync($"Ex Id TRANSIENT: {serv.GetID().ToString()}");
        });
    });

app.Map("/", async (context) =>
{
    var servScoped = context.RequestServices.GetRequiredService<IScopedId>();
    var servSing = context.RequestServices.GetRequiredService<ISingID>();
    var servTransient = context.RequestServices.GetRequiredService<ITransientID>();
    await context.Response.WriteAsync($"Id SINGLETON: {servSing.GetID().ToString()}" +
                                      $"\nId SCOPED: {servScoped.GetID().ToString()}" +
                                      $"\nId TRANSIENT: {servTransient.GetID().ToString()}");
});
*/

//Task_3
app.Map("/start", appBuilder =>
{
    appBuilder.UseWhen(
        context=>context.Request.Query["type"].ToString().Equals("rel2",StringComparison.OrdinalIgnoreCase),
        appBuilder =>
        {
            //Если реализация 2 запускаем медивер который запустит 2 реализацию
            appBuilder.UseMiddleware<ReportMiddleware>();
        }
        );
    appBuilder.Run(async (context) =>
    {
        string type = context.Request.Query["type"].ToString();
        switch (type)
        {
            case "rel1":
            case "Rel1":
                await context.Response.WriteAsync($"Relise:{ context.RequestServices.GetRequiredKeyedService<IReportService>("Rel1").PrintReport()}");
                break;
            
        }
    });
});


app.Run();