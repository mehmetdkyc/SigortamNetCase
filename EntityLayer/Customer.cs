using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        public long TCKN { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int Birthdate { get; set; }
    }
}
