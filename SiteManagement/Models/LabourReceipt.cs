using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Models
{
    public class LabourReceipt
    {
        public int Id { get; set; }
        public int LabourId { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }

        public string Dscription { get; set; }

    }
}
