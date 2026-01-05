namespace KnowledgeAuditor.Api.Core.Models
{
    public sealed class PolicySection
    {

        public string SectionId { get; init; } = default!;
        public string Text { get; init; } = default!;
        public string Document { get; init; } = default!;
        public int PageNumber { get; init; }
    }
}
