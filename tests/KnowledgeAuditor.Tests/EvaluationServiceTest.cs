using KnowledgeAuditor.Api.Core.Models;
using KnowledgeAuditor.Api.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAuditor.Tests
{
    public class EvaluationServiceTest
    {

        [Fact]
    public void Evaluate_Returns_Confidence_When_Evidence_Exists()
        {

            SimpleEvaluationService service = new SimpleEvaluationService();

           var dummyData= TestUtils.GetDummyKnowledge();
           var evalResult = service.Evaluate( dummyData);
              Assert.True(evalResult.HasSupportingEvidence);
              Assert.Equal(1, evalResult.EvidenceCount);
              Assert.Equal(0.5, evalResult.ConfidenceScore);

        }
       
    }
}
