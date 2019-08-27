using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.Models
{
    public class Labour
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name Cannot Exceed 50 Characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Category Cannot Exceed 50 Characters")]
        public string Category { get; set; }

        [MaxLength(50, ErrorMessage = "Phone Cannot Exceed 50 Characters")]
        public string Phone { get; set; }

       
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0")]
        public float Wage { get; set; }

        public ICollection<LabourExpense> LabourExpenses { get; set; }

    }
}
