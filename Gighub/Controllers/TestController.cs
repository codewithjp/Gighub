using Gighub.Data;
using Gighub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Controllers
{
    
    public class TestController : Controller
    {
        private readonly AppDBContext _appDBContext;
        public TestController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }
        public IActionResult Index()
        {
            //var movie1= new Movie
            //{
            //    MovieName = "300", Genre="Action", ReleaseDate=DateTime.Now.AddYears(-40)
            //};
            //var movie2 = new Movie
            //{
            //    MovieName = "Titanic",Genre = "Romance",ReleaseDate = DateTime.Now.AddYears(2)
            //};
            //var actors = new List<Actor>
            //{
            //    new Actor{  Movies= new List<Movie> {movie1 },Name="Marlon Brando",Address="3-D Park Street,Newyork" },
            //    new Actor{  Movies= new List<Movie> {movie1,movie2 },Name="Leonardo Decaprio",Address="32-B High Street,London" },
            //};
            //_appDBContext.AddRange(actors);
            //_appDBContext.SaveChanges();

            var actorwithmovies = _appDBContext.Actors.Include(a => a.Movies.Where(m=>m.MovieName== "Titanic"))
                                .Where(a => a.Name == "Leonardo Decaprio" ).ToList();

            var employeewithDept = _appDBContext.Employees.Include(e => e.Department).ToList();

            //var emp1 = new Employee { DepId = 1, Name = "Emp3", Gender = "G", MobileNo = "9985225400" };
            //var emp2 = new Employee { DepId = 2, Name = "Emp4", Gender = "M", MobileNo = "9780025477" };
            //var dept1 = new Department { DepName = "PR", DepHead = "Mrs. Hilery" };
            //_appDBContext.Departments.Add(dept1);
            //_appDBContext.Employees.AddRange(emp1,emp2);
            //_appDBContext.SaveChanges();
            return View(employeewithDept);
        }
    }
}
