using Workshoppservices.Accounts;
using Microsoft.AspNetCore.Mvc;
using Workshoppservices.Models;
using Microsoft.AspNetCore.Authorization;

namespace Workshoppservices.Controllers
{
    [Authorize]
    public class AccountssController : Controller
    {
        Accountrepository repo;
        public AccountssController()
        {

            repo = new Accountrepository();
        }


        public IActionResult Index()
        {
            var data = repo.getAll();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(services model)
        {
            try
            {
                repo.create(model.ser_name, model.price);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal Server Error", message = ex.Message });
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var found = repo.get_by_id(id);
                return View(found);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal Server Error", message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult Edit(services model)
        {
            try
            {
                repo.update(model.Id, model.ser_name, model.price);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal Server Error", message = ex.Message });
            }
        }

        public IActionResult Delete(int id)
        {


            var found = repo.get_by_id(id);
            return View(found);
        }




        [HttpPost]
        public IActionResult Delete(services model)
        {
            try
            {


                repo.delete(model.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "Internal Server Error", message = ex.Message });
            }

        }


    }
}
    

