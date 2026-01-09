namespace KnowledgeAuditor.Api.Core.Models
{
    public sealed class EvidenceReference
    {

        public string PolicyId { get; init; } = default!;
        public string Document { get; init; } = default!;
        public string SectionId { get; init; } = default!;
        public int PageNumber { get; init; }
    }
}
