using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp1.Models.Data
{
    public class User
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        private string _nameCompany;
        public string Comn { get => _nameCompany; set => _nameCompany = value; }
        public Company Company { get; set; }

    }
}
