using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace WpfApp1.Models.Data.ModelCode
{
    class CompanyContext : DbContext
    {
        public CompanyContext()
            : base("DbCompany")
        { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
