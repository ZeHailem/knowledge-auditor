using KnowledgeAuditor.Api.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
namespace KnowledgeAuditor.Tests;

public class InMemoryHrKnowledgeStoreTests

{

    private readonly Mock<IWebHostEnvironment> _mockEnvironment;
    private readonly string _testContentRootPath;

    public InMemoryHrKnowledgeStoreTests()
    {
        _mockEnvironment = new Mock<IWebHostEnvironment>();
        
        _testContentRootPath = Path.Combine(AppContext.BaseDirectory);
       
        _mockEnvironment.Setup(x => x.ContentRootPath).Returns(_testContentRootPath);
    }

    [Fact]
    public void Loads_Hr_Policies_From_Json_File()
    {
          var policity = new InMemoryHrKnowledgeStore(_mockEnvironment.Object);

        var policies = policity.GetAllPolicies();
        Assert .Equal(2, policies.Count);

        // Additional assertions can be made here based on the expected content of the JSON file
    }


}
