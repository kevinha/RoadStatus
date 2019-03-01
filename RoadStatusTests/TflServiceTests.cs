using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using RoadStatus.Services;

namespace RoadStatus.Tests
{
    public class Tests
    {
        private static readonly ApiConfig ApiConfig = new ApiConfig("http://example.com", "appId", "appKey");
        
        [Test]
        public async Task WhenApiReturns404_ThenResultIsInvalidId()
        {
            // Arrange
            var sut = new TflService(new HttpClient(new Return404NotFoundResponseHandler()), ApiConfig);
            
            // Act
            var result = await sut.GetRoad("invalid id");
            
            // Assert
            result.Successful.Should().BeFalse();
            result.Error.Should().Be(Errors.InvalidId);
        }

        [Test] public async Task WhenApiReturnsMultipleResults_ThenResultIsMultipleResults()
        {
            // Arrange
            var sut = new TflService(new HttpClient(new Return200WithContentResponseHandler("two-roads.json")), ApiConfig);
            
            // Act
            var result = await sut.GetRoad("invalid id");
            
            // Assert
            result.Successful.Should().BeFalse();
            result.Error.Should().Be(Errors.MultipleResults);
        }

        [Test] public async Task WhenApiReturnsOK_ThenDetailsArePopulated()
        {
            // Arrange
            var sut = new TflService(new HttpClient(new Return200WithContentResponseHandler("one-road.json")),
                ApiConfig);
            
            // Act
            var result = await sut.GetRoad("valid id");
            
            // Assert
            result.Successful.Should().BeTrue();
            result.Value.DisplayName.Should().Be("A2");
            result.Value.StatusSeverity.Should().Be("Good");
            result.Value.StatusSeverityDescription.Should().Be("No Exceptional Delays");
        }
    }
    
}