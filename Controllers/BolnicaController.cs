using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BolnicaProjekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BolnicaController:ControllerBase
    {
        public BolnicaContext Context {get;set;}

        public BolnicaController(BolnicaContext context)
        {
            Context=context;
        }

        [Route("DodajBolnicu/{naziv}/{adresa}")]
        [HttpPost]
        public async Task<ActionResult> DodajBolnicu(string naziv,string adresa)
        {
            if(string.IsNullOrWhiteSpace(naziv) || naziv.Length>50)
            {
                return BadRequest("Invalidan unos naziva bolnice!");
            }
            if(string.IsNullOrWhiteSpace(adresa) || adresa.Length>50)
            {
                return BadRequest("Invalidan unos adrese!");
            }
            try{
                var bolnica = new Bolnica();
                bolnica.Naziv=naziv;
                bolnica.Adresa=adresa;
                Context.Bolnice.Add(bolnica);
                await Context.SaveChangesAsync();
                return Ok($"Validan unos bolnice.ID : {bolnica.ID}");
            }        
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromeniBolnicu/{id}/{adresa}")]
        [HttpPut]
        public async Task<ActionResult> PromeniBolnicu(int id,string adresa)  
        {
            if(id<=0)
            {
                return BadRequest("Invalidan unos ID-a bolnice!");
            }
            if(string.IsNullOrWhiteSpace(adresa) || adresa.Length>50)
            {
                return BadRequest("Invalidan unos adrese!");
            }
            try{
                var bolnica = await Context.Bolnice.FindAsync(id);
                if(bolnica==null)
                {
                    return BadRequest($"Ne postoji bolnica sa ID :{id}");
                }
                bolnica.Adresa=adresa;
                await Context.SaveChangesAsync();
                return Ok($"Validno promenjena bolnica sa ID : {id}");
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }     
        [Route("IzbrisiBolnicu/{id}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiBolnicu(int id)
        {
            if(id<=0)
            {
                return BadRequest("Invalidan unos ID bolnice!");
            }
            try{
                var bolnica = await Context.Bolnice.FindAsync(id);
                int temp = id;
                  if(bolnica==null)
                {
                    return BadRequest($"Ne postoji bolnica sa ID :{id}");
                }
                Context.Bolnice.Remove(bolnica);
                await Context.SaveChangesAsync();
                return Ok($"Obrisana bolnica sa ID : {temp}");
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PreuzmiBolnicu/{grad}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiBolnicu(string grad)
        {
            //ODRADI KASNIJE
            if(string.IsNullOrWhiteSpace(grad))
            {
                return BadRequest("Invalidan unos ID bolnice!");
            }
            try{
                var bolnica =  await Context.Bolnice.Where(p=>p.Adresa==grad)
                        .Select(p=>new{
                            p.ID,
                            p.Naziv
                        }).ToListAsync();
                return Ok(bolnica);
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("Gradovi")]
        [HttpGet]
        public async Task<ActionResult> Gradovi()
        {
            var temp = await Context.Bolnice.ToListAsync();
            try{
              return Ok(
                  temp.Select(p=> new{
                      p.Adresa,
                  }
              ));
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
    
}
