using csharp_middlewares.Middlewares;
using csharp_middlewares.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISampleScopeService, SampleScopeService>();
builder.Services.AddTransient<FactoryBasedMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<FactoryBasedMiddleware>();

app.UseMiddleware<RequestLoggingMiddleware>();

// works when http requests hit (Inline middleware)
app.Use(async (context, next) =>
{
    Console.WriteLine("Before next middleware");

    if (!context.User!.Identity!.IsAuthenticated)
    {
        Console.WriteLine("You are not authenticated to perform actions.");
        context.Response.StatusCode = 401;
        return;
    }

    await next();

    Console.WriteLine("After next middleware");
});

app.Run();
