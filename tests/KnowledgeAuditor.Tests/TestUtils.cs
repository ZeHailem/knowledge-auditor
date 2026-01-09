using KnowledgeAuditor.Api.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeAuditor.Tests
{
    internal static class TestUtils
    {
        internal static IReadOnlyList<KnowledgeChunk> GetDummyKnowledge()
        {
            return new List<KnowledgeChunk>
            {
                new()
                {
                    Text = "An employee may be terminated with cause following an internal review.",
                    Metadata = new KnowledgeMetadata
                    {
                        PolicyId = "HR-POL-12",
                        SectionId = "4.2",
                        Document = "employee_handbook.pdf",
                        PageNumber = 18
                    }
                }
            };
        }
    }
}
