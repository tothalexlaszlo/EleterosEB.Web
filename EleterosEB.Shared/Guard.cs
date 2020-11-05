using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EleterosEB.Shared
{
    public class Guard
    {
        public static void ForPrecedesDate(DateTime value, DateTime dateToPrecede, string parameterName)
        {
            if (value >= dateToPrecede)
            {
                throw new ArgumentOutOfRangeException(parameterName);
            }
        }
    }
}
