using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using WebApplication1.Service;

namespace UnitTests.Services;

public class PostalCodeServiceTests
{
    
    [Fact]
    public void GetTop10PostalCodeAmounts_Should_Return_10_Values()
    {
        var mockLogger = new Mock<ILogger<PostalCodeService>>();
        
        // Arrange
        var postalCodeService = new PostalCodeService(mockLogger.Object);
        
        // Act
        var postalCodeAmounts = postalCodeService.GetTop10PostalCodeAmounts("file.csv");
        
        // Assert
        var codeAmounts = postalCodeAmounts.ToList();
        
        Assert.Equal(10, codeAmounts.Count);
        
        var firstPostalCodeAmount = codeAmounts.FirstOrDefault();
        
        Assert.Equal("WC2N", firstPostalCodeAmount?.PostalCode);
        Assert.Equal(125_000_000, firstPostalCodeAmount?.Amount);
    }
    
    
    [Fact]
    public void GetTop10PostalCodeAmounts_Should_Return_No_Value_When_Exception_Is_Thrown()
    {
        var mockLogger = new Mock<ILogger<PostalCodeService>>();
        
        // Arrange
        var postalCodeService = new PostalCodeService(mockLogger.Object);
        
        // Act
        var postalCodeAmounts = postalCodeService.GetTop10PostalCodeAmounts("filessss.csv");
        
        // Assert
        var codeAmounts = postalCodeAmounts.ToList();
        
        Assert.Empty(codeAmounts);
        
        mockLogger.Verify(x=>x.Log(
            LogLevel.Critical,
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<FileNotFoundException>(),
            ((Func<It.IsAnyType, Exception, string>) It.IsAny<object>())!), Times.Once);    
        
       
    }
}