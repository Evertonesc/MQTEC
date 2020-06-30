using MQTEC.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MQTEC.Models;

namespace MQTEC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductRepository _repository;
        public ProductsController(ProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetTableAsync());
        }


        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = _repository.GetCategory();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Save([Bind("Id,Title,Quantity,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.Save(product);
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

            var product = _repository.Edit(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = _repository.GetCategoryEdit(product);
            //    return View(product);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Quantity,Price,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.ConfirmEdit(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = _repository.GetCategoryEdit(product);
            return View(product);
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

        private bool ProductExists(int id)
        {
            return _repository.Exists(id);
        }     
    }
}
