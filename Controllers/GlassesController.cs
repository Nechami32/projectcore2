using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OurApi.Models;
using OurApi.Services;
using OurApi.Interfaces;
using OurApi.Models;
using OurApi.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Glasses.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GlassesController : ControllerBase
    {
        private Iglass glassserice;
        public GlassesController(Iglass glasses)
        {
            this.glassserice = glasses;
        }

        [HttpGet]
   //[Authorize(Policy = "User")]
        public ActionResult< List<Glasse>> GetAll() =>
            glassserice.GetAll(int.Parse(User.FindFirst("id")?.Value!));
                      //  glassserice.GetAll(1);



        [HttpGet("{id}")]
      //  [Authorize(Policy = "User")]

        public ActionResult<Glasse> Get(int id)
        {
            var g = glassserice.Get(id);

            if (g == null)
                return NotFound();

            return g;
        }

        [HttpPost]
     //[Authorize(Policy = "User")]

        public IActionResult Create(Glasse gl)
        {
             var newId = glassserice.Add(gl,int.Parse(User.FindFirst("id")?.Value!));

        return CreatedAtAction("Post",
            new { id = newId}, glassserice.Get(newId));

        }

        [HttpPut("{id}")]
     // [Authorize(Policy = "User")]

        public IActionResult Update(int id, Glasse g)
        {
            if (id != g.id)
                return BadRequest();

            var existing = glassserice.Get(id);
            if (existing is null)
                return NotFound();

            glassserice.Update(g,int.Parse(User.FindFirst("id")?.Value!));

            return NoContent();
        }
     


        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]

        public IActionResult Delete(int id)
        {
            var gla = glassserice.Get(id);
            if (gla is null)
                return NotFound();

            glassserice.Delete(id);

            return Content(glassserice.Count.ToString());
        }
    }
}
