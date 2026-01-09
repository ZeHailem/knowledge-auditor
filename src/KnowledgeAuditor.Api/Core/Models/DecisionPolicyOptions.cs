namespace KnowledgeAuditor.Api.Core.Models;

public sealed class DecisionPolicyOptions
{
    public const string SectionName = "DecisionPolicy";

    public double ApprovalThreshold { get; init; } = 0.6;
}
