using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Models
{
    public class LabourReceipt
    {
        public int Id { get; set; }
        public int LabourId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yy}")]
        public DateTime Date { get; set; }

        [Column(TypeName = "money")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public float Amount { get; set; }

        [MaxLength(250, ErrorMessage = "Description Cannot Exceed 250 Characters")]
        public string Dscription { get; set; }


        public Labour Labour { get; set; }
    }
}
