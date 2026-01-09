using KnowledgeAuditor.Api.Services;
using System;
using System.Collections.Generic;
using System.Text;
using KnowledgeAuditor.Api.Core.Models;

namespace KnowledgeAuditor.Tests
{
    public class PolicyDecisionEngineTests
    {

        [Fact]
        public void PolicyDecisionEngine_tests()
        {
            var knowledge = TestUtils.GetDummyKnowledge();
            var policyDesc = new PolicyDecisionEngine();
            SimpleEvaluationService service = new();
            var evalResult = service.Evaluate(knowledge);
            var dec = policyDesc.Decide(evalResult, knowledge);

            Assert.Single(dec.Evidence);

            // becouse evaluation confidence score is < 0.6 .
            Assert.Equal(RiskLevel.Medium, dec.RiskLevel);
            Assert.Equal(DecisionOutcome.Escalated, dec.Outcome);
        }
    }
}
