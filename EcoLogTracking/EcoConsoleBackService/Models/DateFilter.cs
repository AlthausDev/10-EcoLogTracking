using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoConsoleBackService
{
    public class DateFilter
    {
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }

        public DateFilter(DateTime? dateStart, DateTime? dateEnd)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public DateFilter() { }

    }
}
