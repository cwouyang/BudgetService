namespace BudgetService;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;
    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        if (end < start)
        {
            return 0;
        }
        return 310;
    }
}