using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolaBackService
{
    public class Configuration
    {
        public int Id { get; set; }

        public int Period { get; set; }

        public DateTime DeletedDate { get; set; }

        public Configuration() { }

        public Configuration(int id, int period, DateTime deletedDate)
        {
            Id = id;
            Period = period;
            DeletedDate = deletedDate;
        }

        public bool Delete(Configuration configuration) {
            DateTime nextDelete = configuration.DeletedDate.AddDays(configuration.Period);
            if (DateTime.Now < nextDelete)
            {
                return true;
            }
            return false;
            
        }
    }
}
