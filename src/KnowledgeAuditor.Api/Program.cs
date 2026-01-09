
using KnowledgeAuditor.Api.Core.Models;
using KnowledgeAuditor.Api.Pipeline;
using KnowledgeAuditor.Api.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DecisionPolicyOptions>(
    builder.Configuration.GetSection(DecisionPolicyOptions.SectionName));
// Register services
builder.Services.AddSingleton<IKnowledgeStore, InMemoryHrKnowledgeStore>();
builder.Services.AddSingleton<IRetrievalService, HrRetrievalService>();
builder.Services.AddSingleton<IEvaluationService, SimpleEvaluationService>();
builder.Services.AddSingleton<IPolicyDecisionEngine, PolicyDecisionEngine>();
builder.Services.AddSingleton<IGovernancePipeline, GovernancePipeline>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.MapControllers();

app.Run();
