using System.Diagnostics;
using System.Security.Cryptography;
using static System.Reflection.Metadata.BlobBuilder;

namespace KataPotter;

public class Book
{
    public const int Price = 8;
}


public class Basket
{
    private readonly int _NumberOfTitles = 5;
    private readonly Dictionary<int, decimal> _discounts = new Dictionary<int, decimal>
    {
        { 2, .05m },
        { 3, .10m },
        { 4, .20m},
        { 5, .25m}
    };

    internal int[] StackTheBooks(int[] books)
    {
        var bookStacks = new int[_NumberOfTitles];

        foreach (var book in books)
        {
            bookStacks[book]++;
        }

        return bookStacks;
    }

    public decimal GetPrice(int[] books)
    {
        var bookStacks = StackTheBooks(books);

        decimal totalDiscount = 0 ;

        while (bookStacks.Any(x => x > 0))
        {
            var setSize = bookStacks.Count(x => x > 0);
            var minStackHeight = bookStacks.Where(x => x != 0).Min();

            totalDiscount += GetBookSetDiscount(setSize) * minStackHeight; ;

            for (var i = 0; i < bookStacks.Length; i++)
            {
                if (bookStacks[i] > 0)
                {
                    bookStacks[i] -= minStackHeight;
                }
            }
        }


        var totalPrice = books.Length * Book.Price;
 
        return totalPrice - totalDiscount;
    }

    private decimal GetBookSetDiscount(int setSize)
    {
       if (_discounts.ContainsKey(setSize))
          return setSize * Book.Price * _discounts[setSize];

        return 0;
    }

}

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
        var discounter = new Basket();

        // Act
        var result = discounter.GetPrice(books);

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
        var discounter = new Basket();

        // Act
        var result = discounter.GetPrice(books);

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
        var discounter = new Basket();

        // Act
        var result = discounter.GetPrice(books);

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
        var discounter = new Basket();

        // Act
        var result = discounter.GetPrice(books);


        // Assert
        Assert.Equal(expected, result);
    }
}