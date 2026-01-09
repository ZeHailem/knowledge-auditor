namespace KnowledgeAuditor.Api.Core.Models
{
    public sealed class AuditDecision
    {

        public DecisionOutcome Outcome { get; init; }
        public RiskLevel RiskLevel { get; init; }

        public string Reason { get; init; } = default!;

        public IReadOnlyList<EvidenceReference> Evidence { get; init; }
            = Array.Empty<EvidenceReference>();

        public string AuditId { get; init; } = Guid.NewGuid().ToString();
    }
}
