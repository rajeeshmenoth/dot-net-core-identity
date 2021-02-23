using GroceryAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FruitsController : Controller
    {
        IList<Fruits> frutis = new List<Fruits> {
        new Fruits() {
            Id = 1,
            Name = "Apple"
        },
        new Fruits(){
            Id = 2,
            Name = "Orange"
        },
        new Fruits(){
            Id = 2,
            Name = "Grape"
        }
        };

        [HttpGet]
        [ResponseCache(Duration = 60)]
        public ActionResult GetFruits()
        {
            return Ok(this.frutis);
        }
    }
}
