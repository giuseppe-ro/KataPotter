namespace KataPotter;

public class Class1Tests
{
    [Theory]
    [InlineData(0, new int[] { })]
    [InlineData(8, new[] { 1 })]
    [InlineData(8, new[] { 2 })]
    [InlineData(8, new[] { 3 })]
    [InlineData(8, new[] { 4 })]
    [InlineData(24, new[] { 1, 1, 1 })]
    public void Test_NormalPrice(decimal expected, int[] books)
    {
        // Arrange
        
        // Act
        var result = 0;

        // Assert
        Assert.Equal(expected, result);
    }
    
    
    [Theory]
    [InlineData(8 * 2 * 0.95, new[] { 0, 1 })]
    [InlineData(8 * 3 * 0.9, new[] { 0, 2, 4 })]
    [InlineData(8 * 4 * 0.8, new[] { 0, 1, 2, 4 })]
    [InlineData(8 * 5 * 0.75, new[] { 0, 1, 2, 3, 4 })]
    public void Test_SimpleDiscount(decimal expected, int[] books)
    {
        // Arrange
        
        // Act
        var result = 0;

        // Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(8 + (8 * 2 * 0.95), new[] { 0, 0, 1 })]
    [InlineData(2 * (8 * 2 * 0.95), new[] { 0, 0, 1, 1 })]
    [InlineData((8 * 4 * 0.8) + (8 * 2 * 0.95), new[] { 0, 0, 1, 2, 2, 3 })]
    [InlineData(8 + (8 * 5 * 0.75), new[] { 0, 1, 1, 2, 3, 4 })]
    public void Test_SeveralDiscounts(decimal expected, int[] books)
    {
        // Arrange
        
        // Act
        var result = 0;

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2 * (8 * 4 * 0.8), new[] { 0, 0, 1, 1, 2, 2, 3, 4 })]
    [InlineData(3 * (8 * 5 * 0.75) + 2 * (8 * 4 * 0.8), 
        new[] { 
            0, 0, 0, 0, 0, 
            1, 1, 1, 1, 1, 
            2, 2, 2, 2, 
            3, 3, 3, 3, 3, 
            4, 4, 4, 4 
        })]
    public void Test_EdgeCasesDiscountsCombinations(decimal expected, int[] books)
    {
        // Arrange
        
        // Act
        var result = 0;

        // Assert
        Assert.Equal(expected, result);
    }
}