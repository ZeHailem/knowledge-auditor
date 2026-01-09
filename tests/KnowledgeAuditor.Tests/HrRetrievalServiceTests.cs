using KnowledgeAuditor.Api.Core.Models;
using KnowledgeAuditor.Api.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAuditor.Tests
{
    public class HrRetrievalServiceTests
    {

        [Fact]
        public void Retrieve_Returns_Matching_Policy_Section_When_Question_Is_Relevant()
        {
            // Arrange
            var store = new FakeKnowledgeStore();
            var retrievalService = new HrRetrievalService(store);

            // Act
            var results = retrievalService.Retrieve("employee may be terminated");

            // Assert
            Assert.NotNull(results);
            Assert.Single(results);

            var chunk = results.First();
            Assert.Equal("HR-POL-12", chunk.Metadata.PolicyId);
            Assert.Equal("4.2", chunk.Metadata.SectionId);
        }
    }




    internal sealed class FakeKnowledgeStore : IKnowledgeStore
    {
        public IReadOnlyList<Policy> GetAllPolicies()
        {
            return new List<Policy>
            {
                new() {
                    PolicyId = "HR-POL-12",
                    Title = "Employee Termination Policy",
                    Domain = PolicyDomains.Hr,
                    Sections =
                    [
                        new() {
                            SectionId = "4.2",
                            Text = "An employee may be terminated with cause following an internal review.",
                            Document = "employee_handbook.pdf",
                            PageNumber = 18
                        }
                    ]
                }
            };
        }
    }
}
