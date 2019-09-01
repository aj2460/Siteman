using Microsoft.EntityFrameworkCore;
using SiteManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteManagement.DbLayer
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {

        }

        public DbSet<Site> Sites { get; set; }
        public DbSet<Labour> labours { get; set; }
        public DbSet<MaterialExpense> MaterialExpenses { get; set; }
        public DbSet<LabourExpense> LabourExpenses { get; set; }
        public DbSet<LabourReceipt> labourReceipts { get; set; }

        public DbSet<EmployeeCategory> EmployeeCategories { get; set; }


    }
}
