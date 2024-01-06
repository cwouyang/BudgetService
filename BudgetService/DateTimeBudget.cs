namespace BudgetService;

public class DateTimeBudget
{
    public DateTimeBudget(Budget budget)
    {
        YearMonth = DateTime.ParseExact(budget.YearMonth, "yyyyMM", null);
        Amount = budget.Amount;
    }
    public DateTime YearMonth { get; set; }

    public decimal Amount { get; set; }
}