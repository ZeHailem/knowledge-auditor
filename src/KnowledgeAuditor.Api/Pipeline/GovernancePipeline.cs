using KnowledgeAuditor.Api.Core.Models;
using KnowledgeAuditor.Api.Services;
using Microsoft.AspNetCore.Http.HttpResults;


namespace KnowledgeAuditor.Api.Pipeline
{
    public class GovernancePipeline : IGovernancePipeline
    {

        private readonly IRetrievalService _retrievalService;
        private readonly IEvaluationService _evaluationService;
        private readonly IPolicyDecisionEngine _decisionEngine;
        private readonly ILogger<GovernancePipeline> _logger;


        public GovernancePipeline(
        IRetrievalService retrievalService,
        IEvaluationService evaluationService,
        IPolicyDecisionEngine decisionEngine,
        ILogger<GovernancePipeline> logger)
        {
            _retrievalService = retrievalService;
            _evaluationService = evaluationService;
            _decisionEngine = decisionEngine;
            _logger = logger;
        }
        public AuditDecision Execute(string question)
        {
            // Create an auditId early so all logs can correlate
             var auditId = Guid.NewGuid().ToString();

            using var scope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["AuditId"] = auditId
            });

            if (string.IsNullOrWhiteSpace(question))
            {
                _logger.LogWarning("Audit rejected: empty question.");

                return new AuditDecision
                {
                    Outcome = DecisionOutcome.Rejected,
                    RiskLevel = RiskLevel.High,
                    Reason = "Question is empty or invalid."
                };
            }

            // 1. Retrieve evidence

            var evidence = _retrievalService.Retrieve(question);
            _logger.LogInformation("Audit started. QuestionLength={QuestionLength}", question.Length);

            // 2. Evaluate evidence
            var evaluation = _evaluationService.Evaluate(evidence);

            _logger.LogInformation(
           "Evaluation completed. HasEvidence={HasEvidence} EvidenceCount={EvidenceCount} Confidence={Confidence}",
           evaluation.HasSupportingEvidence,
           evaluation.EvidenceCount,
           evaluation.ConfidenceScore);
            // 3. Decide & enforce
            var decision = _decisionEngine.Decide(evaluation, evidence);

            _logger.LogInformation(
            "Decision enforced. Outcome={Outcome} RiskLevel={RiskLevel}",
            decision.Outcome,
            decision.RiskLevel);

            _logger.LogInformation("Audit finished.");

            return decision;
        }
    }


    public interface IGovernancePipeline
    {
        AuditDecision Execute(string question);
    }
}
