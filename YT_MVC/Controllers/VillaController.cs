using Microsoft.AspNetCore.Mvc;
using YT_MVC.Application.Common.Interfaces;
using YT_MVC.Domain.Entities;
using YT_MVC.Infrastructure.Data;

namespace YT_MVC.Controllers
{

    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)

        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var villas= _unitOfWork.Villa.GetAll();
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
                if(obj.Image!=null)
                {
                    string filename=Guid.NewGuid().ToString()+Path.GetExtension(obj.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"Images\VillaImage");
                    using var filestream = new FileStream(Path.Combine(imagePath, filename), FileMode.Create);
                    obj.Image.CopyTo(filestream);
                    obj.ImageUrl= @"Images\VillaImage\" + filename;
                }
                else
                {
                    obj.ImageUrl = "https://placehold.co/600x400";
                }
                _unitOfWork.Villa.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "The villa has been created successfully";

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Update(int villaid)
        {
            Villa? obj= _unitOfWork.Villa.Get(x => x.Id == villaid);
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
                _unitOfWork.Villa.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "The villa has been Updated successfully";

                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }
        public IActionResult Delete(int villaid)
        {
            Villa? obj = _unitOfWork.Villa.Get(x => x.Id == villaid);
            if (obj is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _unitOfWork.Villa.Get(u=>u.Id == obj.Id);

            if (objFromDb is not null)
            {
                _unitOfWork.Villa.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"]="The villa has been deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["error"] = "The villa could not be deleted";

            return View(obj);
        }
    }
}
