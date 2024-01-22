namespace CoreLogic.Contracts;

public interface IDateTransform
{
    Task<DateOnly> MonthYearToDate1st(string monthYear);
}