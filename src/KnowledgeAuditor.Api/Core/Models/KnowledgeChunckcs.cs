namespace KnowledgeAuditor.Api.Core.Models
{
    public sealed class KnowledgeChunckcs
    {
        public string Text { get; init; } = default!;
        public KnowledgeMetadata Metadata { get; init; } = new();

    }


    public sealed class KnowledgeMetadata
    {
        public string PolicyId { get; init; } = default!;
        public string SectionId { get; init; } = default!;
        public string Document { get; init; } = default!;
        public int PageNumber { get; init; }
    }
}
