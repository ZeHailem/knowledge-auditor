using KnowledgeAuditor.Api.Core.Models;

namespace KnowledgeAuditor.Api.Services
{
    public class SimpleEvaluationService : IEvaluationService
    {
        public EvaluationResult Evaluate(IReadOnlyList<KnowledgeChunk> evidence)
        {
            if (evidence == null || evidence.Count == 0)
            {
                return new EvaluationResult
                {
                    HasSupportingEvidence = false,
                    EvidenceCount = 0,
                    ConfidenceScore = 0.0
                };
            }


            // Simple deterministic scoring (for now)
            var confidence = Math.Min(1.0, evidence.Count * 0.5);

            return new EvaluationResult
            {
                HasSupportingEvidence = true,
                EvidenceCount = evidence.Count,
                ConfidenceScore = confidence
            };
        }
    }


    public class EvaluationResult
    {

        public bool HasSupportingEvidence { get; init; }

        public int EvidenceCount { get; init; }

        public double ConfidenceScore { get; init; }
    }
    public interface IEvaluationService
    {
        EvaluationResult Evaluate(IReadOnlyList<KnowledgeChunk> evidence);
    }
}
