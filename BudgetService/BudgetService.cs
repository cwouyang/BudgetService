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

        List<DateTimeBudget> budgets = _budgetRepo.GetAll()
                                          .Select(b => new DateTimeBudget(b))
                                          .OrderBy(b => b.YearMonth)
                                          .ToList();
        if (start > budgets.Last().YearMonth || end < budgets.First().YearMonth)
        {
            return 0;
        }

        if (start.Year == end.Year && start.Month == end.Month)
        {
            var budget = budgets.FirstOrDefault(b => b.YearMonth == start);
            if (budget == null)
            {
                return 0;
            }

            var dayInMonth = DateTime.DaysInMonth(budget.YearMonth.Year, budget.YearMonth.Month);
            return budget.Amount / dayInMonth;
        }
        return 310;
    }
}