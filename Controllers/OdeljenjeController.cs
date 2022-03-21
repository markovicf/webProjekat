using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BolnicaProjekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OdeljenjeController:ControllerBase
    {
        public BolnicaContext Context {get;set;}

        public OdeljenjeController(BolnicaContext context)
        {
            Context=context;
        }

        [Route("DodajOdeljenje/{naziv}/{spec}")]
        [HttpPost]
        public async Task<ActionResult> DodajOdeljenje(string naziv,string spec)
        {
            if(string.IsNullOrWhiteSpace(naziv) || naziv.Length>50)
            {
                return BadRequest("Invalidan unos naziva!");
            }
             if(string.IsNullOrWhiteSpace(spec) || spec.Length>30)
            {
                return BadRequest("Invalidan unos naziva!");
            }
            try{
                var od = new Odeljenje();
                od.Naziv=naziv;
                od.Specijalizacija=spec;
                Context.Odeljenja.Add(od);
                await Context.SaveChangesAsync();
                return Ok("Validno dodato odeljenje");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromeniOdeljenje/{naziv}/{spec}")]
        [HttpPut]
        public async Task<ActionResult> PromeniOdeljenje(string naziv,string spec)
        {
             if(string.IsNullOrWhiteSpace(naziv) || naziv.Length>50)
            {
                return BadRequest("Invalidan unos naziva!");
            }
             if(string.IsNullOrWhiteSpace(spec) || spec.Length>30)
            {
                return BadRequest("Invalidan unos naziva!");
            }
            try{
                var od = await Context.Odeljenja.Where(p=>p.Naziv==naziv).FirstOrDefaultAsync();
                od.Specijalizacija=spec;
                 if(od==null)
                {
                    return BadRequest("Ne postoji");
                }
                await Context.SaveChangesAsync();
                return Ok($"Validno promenjeno odeljenje");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("IzbrisiOdeljenje/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiOdeljenje(int id)
        {
            if(id<=0)
            {
                return BadRequest("Invalidan unos ID odeljenja!");
            }
            try{
                var od = await Context.Odeljenja.FindAsync(id);
                int temp = id;
                if(od==null)
                {
                    return BadRequest("Ne postoji");
                }
                Context.Odeljenja.Remove(od);
                await Context.SaveChangesAsync();
                return Ok($"Obrisana odeljenje sa ID : {temp}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("PreuzmiOdeljenje/{id}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiOdeljenje(int id)
        {
            if(id<=0)
            {
                return BadRequest("Invalidan unos ID odeljenja!");
            }
            try{
                var temp =  await Context.Odeljenja.Include(p=>p.Bolnica).Where(p=>p.Bolnica.ID==id).
                Select(p=>new{
                    p.ID,
                    p.Naziv
                }).ToListAsync();
                if(temp==null)
                {
                     return BadRequest("Ne postoji takvo odeljenje!");
                }
                return Ok(temp);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

