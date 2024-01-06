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
        Assert.AreEqual(result, 310);
    }
}