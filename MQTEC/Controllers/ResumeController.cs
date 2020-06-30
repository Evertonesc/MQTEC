using System;
using MQTEC.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MQTEC.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ResumeRepository _repository;
        public ResumeController(ResumeRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _repository.GetTableAsync());
            }
            catch (Exception ex)
            {
                ViewBag.ErrorTitle = null;
                return View("Error");
            }

        }
    }
}