using KnowledgeAuditor.Api.Core.Models;
using KnowledgeAuditor.Api.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAuditor.Tests
{
    public class PolicyDecisionEngineTests
    {

        [Fact]
        public void PolicyDecisionEngine_tests()
        {
            var knowledge = TestUtils.GetDummyKnowledge();
            var options = Options.Create(new DecisionPolicyOptions());
            
            var policyDesc = new PolicyDecisionEngine(options);
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
