using TaxAssistant.Declarations;
using TaxAssistant.External.Services;
using TaxAssistant.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILLMService, LLMService>();
builder.Services.ConfigureTerytClient(builder.Configuration);
builder.Services.ConfigureEDeclarationClient(builder.Configuration);
builder.Services.ConfigureLlmClient(builder.Configuration);
builder.Services.RegisterDeclarations();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();