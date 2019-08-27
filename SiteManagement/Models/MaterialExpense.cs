using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Models
{
    public class MaterialExpense
    {
        public int Id { get; set; }
        public int SiteId { get; set; }
        public int LabourId { get; set; }

        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [MaxLength(250, ErrorMessage = "Particular Cannot Exceed 250 Characters")]
        [Required]
        public string Particular { get; set; }

        [MaxLength(250, ErrorMessage = "Description Cannot Exceed 250 Characters")]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public float Amount { get; set; }

        public Labour Labour { get; set; }
        public Site Site { get; set; }


    }

}
