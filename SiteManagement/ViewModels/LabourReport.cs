using SiteManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.ViewModels
{
    public class LabourReport
    {
        public Labour labour { get; set; }
        public IEnumerable<MaterialExpense> materialExpense { get; set; }
        public IEnumerable<LabourExpense> labourExpense { get; set; }
    }
}
