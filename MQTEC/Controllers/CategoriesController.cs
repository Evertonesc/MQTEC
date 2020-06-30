using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQTEC.Models;
using MQTEC.Repositories;

namespace MQTEC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryRepository _repository;
        public CategoriesController(CategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetTableAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Save([Bind("Id,Title")] Category category)
        {
            if (ModelState.IsValid)
            {
                _repository.Save(category);
                TempData["value"] = "Success";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _repository.Edit(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }


        //EXCEPTION => WRONG PARAMETER => CORRECT  Title
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return View("Error");
            }

            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            else
            {
                try
                {
                    await _repository.ConfirmEdit(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }                      
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]string id)
        {
            if (ModelState.IsValid)
            {
                int.TryParse(id, out int result);
                await _repository.Delete(result);

            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            return Json(true);
        }

        private bool CategoryExists(int id)
        {
            return _repository.Exists(id);
        }
    }
}
