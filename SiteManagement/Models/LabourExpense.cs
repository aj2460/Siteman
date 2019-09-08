using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Models
{
    public class LabourExpense
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public int LabourId { get; set; }
        public int ExpenseTypeId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yy}")]
        public DateTime ExpDate { get; set; }

        [MaxLength(250, ErrorMessage = "Particular Cannot Exceed 250 Characters")]
      
        public string Particular { get; set; }


        public string Day { get; set; } // half or full day

        [Column(TypeName = "money")]    
        public float Wage { get; set; }

       
        [MaxLength(250, ErrorMessage = "Description Cannot Exceed 250 Characters")]
        public string Description { get; set; }

        public Labour Labour { get; set; }
        public Site Site { get; set; }
        public ExpenseType ExpenseType { get; set; }

    }
}


