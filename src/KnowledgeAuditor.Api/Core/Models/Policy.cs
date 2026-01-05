namespace KnowledgeAuditor.Api.Core.Models
{
    public sealed class Policy
    {

        public string PolicyId { get; init; } = default!;
        public string Title { get; init; } = default!;
        public PolicyDomain Domain { get; init; }
        public string Region { get; init; } = "US";
        public string Version { get; init; } = "1.0";

        public IReadOnlyList<PolicySection> Sections { get; init; }
            = Array.Empty<PolicySection>();
    }
}
