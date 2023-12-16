using DanskeBank.Api.Exceptions;
using DanskeBank.Api.WordCounter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddScoped<IWordCounterService, WordCounterService>();

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(o => o.EnableTryItOutByDefault());

app.MapControllers();
app.Run();
