using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BolnicaProjekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacijentController:ControllerBase
    {
        public BolnicaContext Context {get;set;}

        public PacijentController(BolnicaContext context)
        {
            Context=context;
        }

        [Route("DodajPacijenta/{ime}/{prezime}/{mail}")]
        [HttpPost]
        public async Task<ActionResult> DodajPacijenta(string ime,string prezime,string mail)
        {
            
            if(string.IsNullOrWhiteSpace(ime) || ime.Length>20)
            {
                return BadRequest("Invalidno ime!");
            }

            if(string.IsNullOrWhiteSpace(prezime) || prezime.Length>20)
            {
                return BadRequest("Invalidno prezime!");
            }

            if(string.IsNullOrWhiteSpace(mail) || mail.Length>40)
            {
                return BadRequest("Invalidan mail!");
            }

    
            try
            {
                var pacijent = new Pacijent();
                pacijent.Ime=ime;
                pacijent.Prezime=prezime;
                pacijent.Mail=mail;
                Context.Pacijenti.Add(pacijent);
                await Context.SaveChangesAsync();
                return Ok(pacijent);
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromeniPacijenta/{id}/{mail}")]
        [HttpPut]
        public async Task<ActionResult> PromeniPacijenta(int id,string mail)
        {
            if(id<=0)
            {
                return BadRequest("Invalidan id!");
            }
            if(string.IsNullOrWhiteSpace(mail) || mail.Length>40)
            {
                return BadRequest("Invalidan mail!");
            }

            try{
                var pacijent = await Context.Pacijenti.Where(p=>p.Mail==mail).FirstOrDefaultAsync();
                if(pacijent==null)
                {
                    return BadRequest("Ne postoji pacijent!");
                }
                pacijent.Mail=mail;
                await Context.SaveChangesAsync();
                return Ok("Uspesno promenjeno");
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [Route("IzbrisiPacijenta/{mail}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiPacijenta(string mail)
        {
            if(string.IsNullOrWhiteSpace(mail) || mail.Length>40)
            {
                return BadRequest("Invalidan mail!");
            }
            try{
                var pacijent = await Context.Pacijenti.Where(p=>p.Mail==mail).FirstOrDefaultAsync();
                if(pacijent==null)
                {
                    return BadRequest("Ne postoji pacijent!");
                }
                Context.Pacijenti.Remove(pacijent);
                await Context.SaveChangesAsync();
                return Ok("Obrisan pacijent");
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PromeniMail/{mail1}/{mail2}")]
        [HttpPut]
        public async Task<ActionResult> PromeniMail(string mail1,string mail2)
        {
            if(string.IsNullOrWhiteSpace(mail1) || IsValid(mail1)==false || string.IsNullOrWhiteSpace(mail1) || IsValid(mail1)==false)
            {
                return BadRequest("Invalidan mail!");
            }

            try{
                var pacijent = await Context.Pacijenti.Where(p=>p.Mail==mail1).FirstOrDefaultAsync();
                if(pacijent==null)
                {
                    return BadRequest("Ne postoji pacijent!");
                }
                pacijent.Mail=mail2;
                await Context.SaveChangesAsync();
                return Ok("Uspesno promenjeno");
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    [NonAction]
    public bool IsValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);

            return true;
        }
        catch (System.FormatException)
        {
            return false;
        }
    }
        //GET METODA
    }

    
    
}
