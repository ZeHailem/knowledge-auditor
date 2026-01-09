using KnowledgeAuditor.Api.Core.Models;
using KnowledgeAuditor.Api.Services;

namespace KnowledgeAuditor.Api.Pipeline
{
    public class GovernancePipeline : IGovernancePipeline
    {

        private readonly IRetrievalService _retrievalService;
        private readonly IEvaluationService _evaluationService;
        private readonly IPolicyDecisionEngine _decisionEngine;


        public GovernancePipeline(
        IRetrievalService retrievalService,
        IEvaluationService evaluationService,
        IPolicyDecisionEngine decisionEngine)
        {
            _retrievalService = retrievalService;
            _evaluationService = evaluationService;
            _decisionEngine = decisionEngine;
        }
        public AuditDecision Execute(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                return new AuditDecision
                {
                    Outcome = DecisionOutcome.Rejected,
                    RiskLevel = RiskLevel.High,
                    Reason = "Question is empty or invalid."
                };
            }

            // 1. Retrieve evidence
            var evidence = _retrievalService.Retrieve(question);

            // 2. Evaluate evidence
            var evaluation = _evaluationService.Evaluate(evidence);

            // 3. Decide & enforce
            var decision = _decisionEngine.Decide(evaluation, evidence);

            return decision;
        }
    }


    public interface IGovernancePipeline
    {
        AuditDecision Execute(string question);
    }
}
