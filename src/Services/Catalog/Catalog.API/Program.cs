using Catalog.API.DataSeed;
using Core.Common.Behaviours;
using Core.Common.Exceptions.Handler;


var builder = WebApplication.CreateBuilder(args);

// Service Registration
builder.Services.AddCarter();
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
    options.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    options.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("dbSource")!);
}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
    builder.Services.InitializeMartenWith<ProductDataSeed>();

builder.Services.AddExceptionHandler<HttpExceptionHandler>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services
    .AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("dbSource")!);

var app = builder.Build();

// Middleware Pipeline
app.MapCarter();

app.UseExceptionHandler(opt => {});

app.UseHealthChecks("/health",new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });

app.Run();
