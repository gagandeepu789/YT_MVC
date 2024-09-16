using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YT_MVC.Application.Common.Interfaces;
using YT_MVC.Domain.Entities;
using YT_MVC.Infrastructure.Data;
using YT_MVC.ViewModels;

namespace YT_MVC.Controllers
{

    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VillaNumberController(IUnitOfWork unitOfWork)

        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var villaNumbers = _unitOfWork.VillaNumber.GetAll(includeProperties:"Villa");
            return View(villaNumbers);
        }
        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };
           
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
           // ModelState.Remove("Villa");
           bool roomNumberExists=_unitOfWork.VillaNumber.Any(u=>u.Villa_Number==obj.VillaNumber.Villa_Number);
          
            if (ModelState.IsValid && !roomNumberExists)
            {
                _unitOfWork.VillaNumber.Add(obj.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa Number has been created successfully";
                return RedirectToAction(nameof(Index));
            }
            if(roomNumberExists)
            {
                TempData["error"] = "The villa number already exists";
            }

            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return View(obj);
        }
        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber= _unitOfWork.VillaNumber.Get(u=>u.Villa_Number==villaNumberId)
            };
            //Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaid);
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {
            

            if (ModelState.IsValid)
            {
                _unitOfWork.VillaNumber.Update(villaNumberVM.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa Number has been updated successfully";
                return RedirectToAction(nameof(Index));
            }


            villaNumberVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
           

            return View(villaNumberVM);
        }
        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)
            };
            //Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaid);
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            VillaNumber? objFromDb = _unitOfWork.VillaNumber
                .Get(u => u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);

            if (objFromDb is not null)
            {
                _unitOfWork.VillaNumber.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The villa  number has been deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The villa could not be deleted";

            return View();
        }
    }
}
