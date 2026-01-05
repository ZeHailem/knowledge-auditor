using KnowledgeAuditor.Api.Core.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace KnowledgeAuditor.Api.Services
{
    public class InMemoryHrKnowledgeStore : IKnowledgeStore
    {
        private readonly IReadOnlyList<Policy> _policies;
        private static readonly JsonSerializerOptions CachedJsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false) }
        };

        public InMemoryHrKnowledgeStore(IWebHostEnvironment env)
        {
            var knowledgePath = Path.Combine(
                env.ContentRootPath,
                "Knowledge",
                "hr-policies.json"
            );

            if (!File.Exists(knowledgePath))
            {
                throw new FileNotFoundException(
                    $"HR policies file not found at path: {knowledgePath}"
                );
            }

            var json = File.ReadAllText(knowledgePath);

            _policies = JsonSerializer.Deserialize<List<Policy>>(json, CachedJsonSerializerOptions)
                    ?? throw new InvalidOperationException(
                        "Failed to deserialize HR policies"
                    );
        }

        public IReadOnlyList<Policy> GetAllPolicies()
        {
            return _policies;
        }
    }


    public interface IKnowledgeStore
    {
        IReadOnlyList<Policy> GetAllPolicies();
    }
}
