using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Models
{
    public class LabourExpense
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public int LabourId { get; set; }

        public DateTime Date { get; set; }
        public int Day { get; set; } // half or full day
        public float Wage { get; set; }

        public string Description { get; set; }
    }
}


