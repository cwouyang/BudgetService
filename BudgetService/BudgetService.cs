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
            var budget = budgets.SingleOrDefault(b => b.YearMonth == start);
            if (budget == null)
            {
                return 0;
            }

            var dayInMonth = DateTime.DaysInMonth(budget.YearMonth.Year, budget.YearMonth.Month);
            return budget.Amount / dayInMonth * ((end - start).Days+1);
        }
        
        var startBudget = budgets.SingleOrDefault(b => b.YearMonth.Year == start.Year && b.YearMonth.Month == start.Month);
        var startDayInMonth = DateTime.DaysInMonth(startBudget.YearMonth.Year, startBudget.YearMonth.Month);
        var startAmount =  startBudget.Amount / startDayInMonth * (((startBudget.YearMonth.AddMonths(1).AddDays(-1)) - start).Days+1);
        
        var endBudget = budgets.SingleOrDefault(b => b.YearMonth.Year == end.Year && b.YearMonth.Month == end.Month);
        var endDayInMonth = DateTime.DaysInMonth(endBudget.YearMonth.Year, endBudget.YearMonth.Month);
        var endAmount =  endBudget.Amount / endDayInMonth * ((end - endBudget.YearMonth).Days+1);
        return startAmount + endAmount;
    }
    }
}