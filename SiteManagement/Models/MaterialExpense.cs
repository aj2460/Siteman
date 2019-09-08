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

        public MaterialExpense()
        {
            this.Date = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        public int SiteId { get; set; }

        [Required]
        public int LabourId { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yy}")]
        public DateTime Date { get; set; }


        [MaxLength(250, ErrorMessage = "Invoice Cannot Exceed 250 Characters")]
        public string InvoiceNo { get; set; }

        [MaxLength(250, ErrorMessage = "Supplier Cannot Exceed 250 Characters")]
        public string Supplier { get; set; }

        [MaxLength(250, ErrorMessage = "Description Cannot Exceed 250 Characters")]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public float Amount { get; set; }

        public Labour Labour { get; set; }
        public Site Site { get; set; }
       

    }

}
