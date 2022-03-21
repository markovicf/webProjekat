using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BolnicaProjekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoktorController:ControllerBase
    {
        public BolnicaContext Context {get;set;}

        public DoktorController(BolnicaContext context)
        {
            Context=context;
        }
    
    
        [Route("DodajDoktora/{ime}/{prezime}")]
        [HttpPost]
        public async Task<ActionResult> DodajDoktora(string ime,string prezime)
        {
            if(string.IsNullOrWhiteSpace(ime) || ime.Length>20)
            {
                return BadRequest("Nevalidan unos imena!");
            }
            if(string.IsNullOrWhiteSpace(prezime) || prezime.Length>20)
            {
                return BadRequest("Nevalidan unos imena!");
            }
            try{
            var doca = new Doktor();
            doca.Ime=ime;
            doca.Prezime=prezime;
            Context.Doktori.Add(doca);
            await Context.SaveChangesAsync();
            return Ok($"Validan unos doktora sa ID : {doca.ID}");
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromeniDoktora/{id}/{prezime}")]
        [HttpPut]
        public async Task<ActionResult> PromeniDoktora(int id,string prezime)
        {
            if(id<=0)
            {
                return BadRequest("Invalidan unos ID-a bolnice!");
            }
            if(string.IsNullOrWhiteSpace(prezime) || prezime.Length>20)
            {
                return BadRequest("Invalidan unos prezimena!");
            }
            try{
                var doca = await Context.Doktori.FindAsync(id);
                if(doca==null)
                {
                    return BadRequest("Ne postoji takav doktor");
                }
                doca.Prezime=prezime;
                await Context.SaveChangesAsync();
                return Ok("Validno promenjen doca");
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisiDoktora/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiDoktora(int id)
        {
            if(id<=0)
            {
                return BadRequest("Invalidan unos ID-a doktora!");
            }
            try{
                var temp = await Context.Doktori.FindAsync(id);
                if(temp==null)
                {
                    return BadRequest("Ne postoji takav doktor");
                }
                Context.Doktori.Remove(temp);
                await Context.SaveChangesAsync();
                return Ok($"Obrisana doktor sa ID : {id}");
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [Route("PreuzmiDocu/{id}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiDoktora(int id)
        {
            //continue
            if(id<=0)
            {
                return BadRequest("Invalidan unos ID doktora!");
            }
            try{
                var doca =  await Context.Doktori.Include(p=>p.Odeljenje).Where(p=>p.Odeljenje.ID==id)
                .Select(p=>new{
                    p.ID,
                    p.Ime,
                    p.Prezime
                }).ToListAsync();
                if(doca==null)
                {
                     return BadRequest("Ne postoji takva doktor!");
                }
                return Ok(doca);
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}