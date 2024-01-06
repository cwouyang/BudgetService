namespace BudgetService;

using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;

using NSubstitute;

public class Tests
{
    private readonly IBudgetRepo _budgetRepo = Substitute.For<IBudgetRepo>();
    [SetUp] public void Setup() { }

    [Test]
    public void QueryWholeMonthAndReturnBudget()
    {
        _budgetRepo
            .GetAll()
            .Returns(new List<Budget>
            {
                new Budget("202401", 310),
                new Budget("202402", 2900)
            });
        var service = new BudgetService(_budgetRepo);

        var result = service.Query(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31));
        Assert.AreEqual(310, result);
    }
    
    [Test]
    public void QueryInvalidPeriod()
    {
        _budgetRepo
            .GetAll()
            .Returns(new List<Budget>
            {
                new Budget("202401", 310),
                new Budget("202402", 2900)
            });
        var service = new BudgetService(_budgetRepo);

        var result = service.Query(new DateTime(2024, 1, 31), new DateTime(2024, 1, 1));
        Assert.AreEqual(0, result);
    }
    
    [Test]
    public void QuerySingleDayAndReturnNoBudget()
    {
        _budgetRepo
            .GetAll()
            .Returns(new List<Budget>
            {
                new Budget("202401", 310),
                new Budget("202402", 2900)
            });
        var service = new BudgetService(_budgetRepo);

        var result = service.Query(new DateTime(2024, 3, 1), new DateTime(2024, 3, 1));
        Assert.AreEqual(0, result);
    }
    
    [Test]
    public void QuerySingleDayAndReturnPartialBudget()
    {
        _budgetRepo
            .GetAll()
            .Returns(new List<Budget>
            {
                new Budget("202401", 310),
                new Budget("202402", 2900)
            });
        var service = new BudgetService(_budgetRepo);

        var result = service.Query(new DateTime(2024, 1, 1), new DateTime(2024, 1, 1));
        Assert.AreEqual(10, result);
    }
    
    [Test]
    public void QueryTwoDaysAndReturnPartialBudget()
    {
        _budgetRepo
            .GetAll()
            .Returns(new List<Budget>
            {
                new Budget("202401", 310),
                new Budget("202402", 2900)
            });
        var service = new BudgetService(_budgetRepo);

        var result = service.Query(new DateTime(2024, 1, 1), new DateTime(2024, 1, 2));
        Assert.AreEqual(10*2, result);
    }
    
    [Test]
    public void QueryCrossMonthAndReturnBudget()
    {
        _budgetRepo
            .GetAll()
            .Returns(new List<Budget>
            {
                new Budget("202401", 310),
                new Budget("202402", 2900)
            });
        var service = new BudgetService(_budgetRepo);
    
        var result = service.Query(new DateTime(2024, 1, 31), new DateTime(2024, 2, 1));
        Assert.AreEqual(10+100, result);
    }
}