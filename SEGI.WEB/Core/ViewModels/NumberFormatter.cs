using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.Core.ViewModels
{
    public static class NumberFormatter
    {
        public static string FormatNumber(decimal number)
        {
            if (number >= 1_000_000)
            {
                return (number / 1_000_000).ToString("0.##") + "M";
            }
            else if (number >= 1_000)
            {
                return (number / 1_000).ToString("0.##") + "K";
            }
            return number.ToString();
        }
    }
}
