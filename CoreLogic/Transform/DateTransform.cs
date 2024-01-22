using CoreLogic.Contracts;

namespace CoreLogic.Transform;

public class DateTransform : IDateTransform
{
    private Dictionary<string, int> monthValuePairs = new Dictionary<string, int>
    {
        { "JAN", 1 }, { "FEB", 2 }, { "MAR", 3 },
        { "APR", 4 }, { "MAY", 5 }, { "JUN", 6 },
        { "JUL", 7 }, { "AUG", 8 }, { "SEP", 9 },
        { "OCT", 10 }, { "NOV", 11 }, { "DEC", 12 }
    };
    public Task<DateOnly> MonthYearToDate1st(string monthYear)
    {
        if (string.IsNullOrEmpty(monthYear) || monthYear.Length < 5)
            throw new ArgumentException("Input string must be at least 5 characters long", nameof(monthYear));

        var month = monthYear.Substring(0, 3).ToUpper();
        var year = monthYear.Substring(3, 2);

        if (monthValuePairs.TryGetValue(month, out int monthValue))
        {
            // Assuming year is provided in two digits, adjust logic if it's in four digits
            var currentYear = DateTime.Now.Year % 100; // Last two digits of the current year
            var yearValue = int.Parse(year) + (int.Parse(year) <= currentYear ? 2000 : 1900);

            var date = new DateOnly(yearValue, monthValue, 1);
            return Task.FromResult(date);
        }
        else
        {
            throw new ArgumentException("Invalid month abbreviation", nameof(monthYear));
        }
    }
}