using System;

namespace Helper
{
    public class DateTimeHelper
    {
        public static DateTime NowTurkey { get { return TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time")); } }
    }
}
