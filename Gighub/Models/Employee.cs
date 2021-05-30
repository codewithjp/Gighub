using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gighub.Models
{
    public class Employee
    {
        [Key]
        public int Empid { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }

        public int DepId { get; set; }

        [ForeignKey("DepId")]
        public Department Department { get; set; }
    }

    public class Department
    {
        [Key]
        public int DepId { get; set; }
        public string DepName { get; set; }
        public string DepHead { get; set; }
        public ICollection<Employee> Employees { get; set; }


    }

    public  class Movie
    {
        [Key]
        public int Id { get; set; }
        public string MovieName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public ICollection<Actor> Actors { get; set; }
    }

    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
