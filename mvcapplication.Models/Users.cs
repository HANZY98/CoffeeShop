using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvcapplication.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public string AboutMe { get; set; }

        public string  JobTitle { get; set; }   
        public string Salary { get; set; }
    }
}
