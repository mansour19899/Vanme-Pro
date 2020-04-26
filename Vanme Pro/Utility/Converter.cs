using System;

namespace Vanme_Pro.Utility
{
    public static class myConverter
    {
        public static decimal ToDecimal(this string txt)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                return 0m;
            }
            else
            {
                return Convert.ToDecimal(txt);
            }

        }

    }
}