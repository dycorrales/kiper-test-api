using System;
using System.Collections.Generic;

namespace Kiper.Condominio.Core.Helpers.Utils
{
    public static class Miscellaneous
    {
        public static string GetRandomExaColor
        {
            get
            {
                var random = new Random();
                return string.Format("#{0:X6}", random.Next(0x1000000));
            }
        }

        public static IEnumerable<DateTime> EachCalendarByPeriod(RecurrencyPeriodType periodType, DateTime startDate, DateTime endDate)
        {
            switch (periodType)
            {
                case RecurrencyPeriodType.Daily:
                    {
                        for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(1)) yield
                        return date;
                        break;
                    }
                case RecurrencyPeriodType.Monthly:
                    {
                        for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddMonths(1)) yield
                        return date;
                        break;
                    }
                case RecurrencyPeriodType.Weekly:
                    {
                        for (var date = startDate.Date; date.Date <= endDate.Date; date = date.AddDays(7)) yield
                        return date;
                        break;
                    }
            }
        }
    }
}
