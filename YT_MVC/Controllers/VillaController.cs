using Microsoft.AspNetCore.Mvc;
using YT_MVC.Domain.Entities;
using YT_MVC.Infrastructure.Data;

namespace YT_MVC.Controllers
{

    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)

        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villas=_db.Villas.ToList();
            return View(villas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", " The description and name cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Villas.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been created successfully";

                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }
        public IActionResult Update(int villaid)
        {
            Villa? obj= _db.Villas.FirstOrDefault(x => x.Id == villaid);
            if (obj == null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Villa obj)
        {
         
            if (ModelState.IsValid && obj.Id>0)
            {
                _db.Villas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been Updated successfully";

                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }
        public IActionResult Delete(int villaid)
        {
            Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaid);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb= _db.Villas.FirstOrDefault(u=>u.Id == obj.Id);

            if (objFromDb is not null)
            {
                _db.Villas.Remove(objFromDb);
                _db.SaveChanges();
                TempData["success"]="The villa has been deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The villa could not be deleted";

            return View(obj);
        }
    }
}
