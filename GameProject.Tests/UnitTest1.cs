using FluentAssertions;

namespace GameProject.Tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var a = 5;
        var b = 10;

        // Act
        var result = a + b;

        // Assert
        result.Should().Be(15);
    }
}