using System.Globalization;

namespace Projekt_Avancerad_.NET.Services
{
    public class HelpMethods
    {
        public static DateTime FirstDateOfWeekStandard(int year, int week)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;

            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear
                (jan1, CultureInfo.CurrentCulture.DateTimeFormat.CalendarWeekRule,
                CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);

            if (firstWeek <= 1)
            {
                week -= 1;
            }
            return firstWeekDay.AddDays(week * 7);

        }
    }
}
