using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Helpers
{
    public static class Extensions
    {
        public static DateTime AddWeeks(this DateTime x, int weeksCount)
        {
            return x.AddDays(weeksCount * 7);
        }
    }
}
