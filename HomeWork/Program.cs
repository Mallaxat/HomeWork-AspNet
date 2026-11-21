var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();

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



app.Run();