using Repository.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Repository.Data;
using Microsoft.AspNetCore.Authorization;

namespace GroceryAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class FruitsController : Controller
    {
        private readonly GroceryDbContext _groceryDbContext;
        public FruitsController(GroceryDbContext groceryDbContext)
        {
            _groceryDbContext = groceryDbContext;
        }

        [HttpGet]
        [Route("GetFruits")]
        [ResponseCache(Duration = 60)]
        public ActionResult GetFruits()
        {
            return Ok(_groceryDbContext.LoadGroceries());
        }
    }
}
