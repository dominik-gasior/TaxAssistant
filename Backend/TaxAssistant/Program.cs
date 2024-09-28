using TaxAssistant.Declarations;
using TaxAssistant.Extensions;
using TaxAssistant.Extensions.Middlewares;
using TaxAssistant.External.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILLMService, LLMService>();
builder.Services.ConfigureTerytClient(builder.Configuration);
builder.Services.ConfigureEDeclarationClient(builder.Configuration);
builder.Services.ConfigureLlmClient(builder.Configuration);
builder.Services.RegisterDeclarations();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Front",
        b => b
            .WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();
app.UseCors("Front");
app.MapControllers();

app.MapGet("/", () => "Hello from TaxAssistant");

app.Run();