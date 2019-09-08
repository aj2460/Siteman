using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Models
{
    public class Site
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Name Cannot Exceed 50 Characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Address Cannot Exceed 250 Characters")]
        public string Address { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Place Cannot Exceed 250 Characters")]
        public string Place { get; set; }

        [MaxLength(50, ErrorMessage = "Phone Cannot Exceed 50 Characters")]
        public string Phone { get; set; }

        [Display(Name = "Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9_.-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email format")]
        public string email { get; set; }

        public ICollection<MaterialExpense> MaterialExpenses { get; set; }
        public ICollection<LabourExpense> LabourExpenses { get; set; }

    }
}
