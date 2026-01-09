using KnowledgeAuditor.Api.Core.Models;

namespace KnowledgeAuditor.Api.Services
{
    public class PolicyDecisionEngine : IPolicyDecisionEngine
    {
        public AuditDecision Decide(EvaluationResult evaluation, IReadOnlyList<KnowledgeChunk> evidence)
        {
        // Case 1: No evidence → Reject
        if (!evaluation.HasSupportingEvidence)
            {
                return new AuditDecision
                {
                    Outcome = DecisionOutcome.Rejected,
                    RiskLevel = RiskLevel.High,
                    Reason = "No supporting company policy found for this request."
                };
            }

            // Case 2: Weak confidence → Escalate
            if (evaluation.ConfidenceScore < 0.6)
            {
                return new AuditDecision
                {
                    Outcome = DecisionOutcome.Escalated,
                    RiskLevel = RiskLevel.Medium,
                    Reason = "Supporting policy found, but confidence is below approval threshold.",
                    Evidence = BuildEvidence(evidence)
                };
            }

            // Case 3: Strong evidence → Approve
            return new AuditDecision
            {
                Outcome = DecisionOutcome.Approved,
                RiskLevel = RiskLevel.Low,
                Reason = "Request is supported by company policy.",
                Evidence = BuildEvidence(evidence)
            };
        }


        private static IReadOnlyList<EvidenceReference> BuildEvidence(
        IReadOnlyList<KnowledgeChunk> evidence)
        {
            return [.. evidence.Select(e => new EvidenceReference
            {
                PolicyId = e.Metadata.PolicyId,
                SectionId = e.Metadata.SectionId,
                Document = e.Metadata.Document,
                PageNumber = e.Metadata.PageNumber
            })];
        }
    }


    public interface IPolicyDecisionEngine
    {
        AuditDecision Decide(
            EvaluationResult evaluation,
            IReadOnlyList<KnowledgeChunk> evidence);
    }
}
