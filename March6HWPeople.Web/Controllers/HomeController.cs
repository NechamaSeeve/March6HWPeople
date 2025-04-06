
using March6HWPeople.Web.Models;
using March6People.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace March6HWPeople.Web.Controllers
{
    public class HomeController : Controller
    {

        private string _connectionString = @"Data Source=10.211.55.2; Initial Catalog=Jan22;User Id=sa;Password=Foobar1@;TrustServerCertificate=true;";

        public IActionResult Index()
        {
            var db = new PersonDb(_connectionString);
            var vm = new PeopleViewModel
            {
                People = db.GetPeople()
            };
            if (TempData["message"] != null)
            {
                vm.Message = (string)TempData["message"];
            }
            return View(vm);
        }
        public IActionResult AddPeople()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPeople(List<Person> people)
        {
            var db = new PersonDb(_connectionString);
            db.Add(people);
            int counter = 0;
            foreach(Person p in people)
            {
                if (p.FirstName != null || p.LastName != null)
                {
                    counter++;
                }
            }
            if (people[0].FirstName != null || people[0].LastName != null)
            {
                if (counter > 1)
                {
                    TempData["message"] = $"{counter} people added successfully";
                }
                else if (counter == 1)
                {
                    TempData["message"] = $"{counter} Person added successfully";
                }
            }
            




            return Redirect("/");
        }
        [HttpPost]
        public IActionResult DeleteAll(List<int> ids)
        {
            if (ids.Count > 1)
            {
                TempData["message"] = $"{ids.Count} people deleted";
            }
            else if (ids.Count == 1)
            {
                TempData["message"] = $"{ids.Count} Person deleted";
            }

            var db = new PersonDb(_connectionString);
            db.DeleteAll(ids);
            return Redirect("/");
        }
      
    }
}
