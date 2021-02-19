using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryAPI.Controllers
{
    public class FruitController : Controller
    {
        // GET: FruitController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FruitController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FruitController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FruitController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FruitController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FruitController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FruitController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FruitController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
