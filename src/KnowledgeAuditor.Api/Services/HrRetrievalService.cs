using KnowledgeAuditor.Api.Core.Models;

namespace KnowledgeAuditor.Api.Services
{
    public class HrRetrievalService(IKnowledgeStore knowledgeStore) : IRetrievalService
    {
        private readonly IKnowledgeStore _knowledgeStore = knowledgeStore;

        public IReadOnlyList<KnowledgeChunk> Retrieve(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                return Array.Empty<KnowledgeChunk>();
            }

            var normalizedQuestion = question.ToLowerInvariant();
            var results = new List<KnowledgeChunk>();

            foreach (var policy in _knowledgeStore.GetAllPolicies())
            {
                foreach (var section in policy.Sections)
                {
                    if (SectionMatches(section.Text, normalizedQuestion))
                    {
                        results.Add(new KnowledgeChunk
                        {
                            Text = section.Text,
                            Metadata = new KnowledgeMetadata
                            {
                                PolicyId = policy.PolicyId,
                                SectionId = section.SectionId,
                                Document = policy.Title,
                                PageNumber = section.PageNumber
                            }
                        });
                    }
                }
            }

            bool SectionMatches(string sectionText, string question)
            {
                var normalizedSectionText = sectionText.ToLowerInvariant();
                return normalizedSectionText.Contains(normalizedQuestion);
            }

            return results;
        }
           

        

    }



    public interface IRetrievalService
    {
        IReadOnlyList<KnowledgeChunk> Retrieve(string question);
    }
}
