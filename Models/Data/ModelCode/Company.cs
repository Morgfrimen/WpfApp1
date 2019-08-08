using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models.Data
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public ICollection<User> Users { get; set; }
        public Company()
        {
            Users = new List<User>();
        }


    }
}
