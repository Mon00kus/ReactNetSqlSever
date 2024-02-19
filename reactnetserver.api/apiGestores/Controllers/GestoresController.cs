using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using apiGestores.Context;
using apiGestores.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
// Got to study the posibility to change ActionResult --> IActionresult
namespace apiGestores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestoresController : Controller
    {

        private readonly AppDbContext _context;
        public GestoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<GestoresController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_context.Gestor.ToList());
            }
            catch (Exception ex)
            {

                return BadRequest($"{ex.Message}");
            }
        }

        // GET api/<GestoresController>/5
        [HttpGet("{id}", Name ="GetGestor")]
        public ActionResult Get(int id)
        {
            try
            {
                var gestor = _context.Gestor.FirstOrDefault(g=> g.Id == id);
                if (gestor == null)
                {
                    return NotFound(new { Message = "Gestor con Id <<" + id + ">> No encontrado" });
                }
                return Ok(gestor);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // POST api/<GestoresController>
        [HttpPost]
        public ActionResult Post([FromBody] Gestor gestor)
        {
            try
            {
                _context.Gestor.Add(gestor);
                _context.SaveChanges();
                return CreatedAtRoute("GetGestor", new { id = gestor.Id }, gestor );
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        // PUT api/<GestoresController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Gestor gestor)
        {
            try
            {
                if (gestor.Id == id)
                {
                    _context.Entry(gestor).State = EntityState.Modified;
                    _context.SaveChanges();
                    return CreatedAtRoute("GetGestor", new { id = gestor.Id }, gestor);
                }
                else
                {
                    return BadRequest(new {Message = "Id No encontrado"});
                }
            }
            catch (Exception ex)
            {

                return BadRequest($"{ex.Message}");
            }
        }

        // DELETE api/<GestoresController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var gestor = _context.Gestor.FirstOrDefault(g=> g.Id == id);
                if (gestor != null)
                {
                    _context.Gestor.Remove(gestor);
                    _context.SaveChanges();
                    return Ok(new { id = id, Message="Gestor removido de BD" });
                }
                else
                {
                    return BadRequest(new { Message = "Id No encontrado" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
