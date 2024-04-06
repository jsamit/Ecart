

var builder = WebApplication.CreateBuilder(args);

// Service Registration
builder.Services.AddCarter();
builder.Services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddMarten(opt =>
{
    opt.Connection(builder.Configuration.GetConnectionString("dbSource")!);
}).UseLightweightSessions();

var app = builder.Build();

// Middleware Pipeline
app.MapCarter();

app.Run();
