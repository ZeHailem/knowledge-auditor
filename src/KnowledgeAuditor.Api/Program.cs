
using KnowledgeAuditor.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<IKnowledgeStore, InMemoryHrKnowledgeStore>();

var app = builder.Build();

// TEMPORARY TEST ENDPOINT
app.MapGet("/debug/policies", (IKnowledgeStore store) =>
{
    var policies = store.GetAllPolicies();

    return Results.Ok(new
    {
        Count = policies.Count,
        PolicyIds = policies.Select(p => p.PolicyId)
    });
});

app.Run();
