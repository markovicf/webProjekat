using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Models;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;

namespace webProjekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpojController:ControllerBase
    {
        public BolnicaContext Context {get;set;}

        public SpojController(BolnicaContext context)
        {
            Context=context;
        }
        [Route("DodajSpoj/{datum}/{idDoktora}/{ime}/{prezime}/{gmail}")]
        [HttpPost]
        public async Task<ActionResult> DodajSpoj(string datum,int idDoktora,string ime,string prezime,string gmail)
        {
            if(idDoktora<=0)
            {
                return BadRequest("Invalidan id doktora");
            }
            if(string.IsNullOrWhiteSpace(ime))
            {
                return BadRequest("Invalidno ime");
            }
            if(string.IsNullOrWhiteSpace(prezime))
            {
                return BadRequest("Invalidno prezime");
            }
            if(string.IsNullOrWhiteSpace(gmail)||IsValid(gmail)==false)
            {
                return BadRequest("Invalidno ime");
            }
            try{
                             /*var temp = await Context.Spojevi
            .Include(p=>p.Doktor).Where(p=>p.Doktor.ID==idDoktora)
            .Where(p=>p.Datum==vreme).FirstOrDefaultAsync(); za kasnije da ne moze na isti datum*/
                var doca = await Context.Doktori.FindAsync(idDoktora);
                var pac = await Context.Pacijenti.Where(p=>p.Mail==gmail).FirstOrDefaultAsync();
                if(pac==null)
                {
                    pac = new Pacijent();
                    pac.Ime=ime;
                    pac.Prezime=prezime;
                    pac.Mail=gmail;
                }

                string temp = datum+":00:00";
                DateTime vr = DateTime.Parse(temp);

                Spoj s = new Spoj();
                s.Doktor=doca;
                s.Pacijent=pac;
                s.Datum =vr;
                Context.Spojevi.Add(s);
                await Context.SaveChangesAsync();
                return Ok(new{
                    s.Datum,
                    s.Doktor,
                    s.Pacijent,
                    s.ID
                });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PreuzmiSpoj/{email}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiSpoj(string email)
        {
            if(string.IsNullOrWhiteSpace(email) || IsValid(email)==false)
            {
                return BadRequest("Neispravan email");
            }
            try{
                var spoj = await Context.Spojevi
                    .Include(p=>p.Doktor)
                    .Include(p=>p.Pacijent).Where(p=>p.Pacijent.Mail==email)
                    .Select(p=>new{
                        p.Pacijent.ID,
                        p.Datum,
                        p.Doktor.Ime,
                        p.Doktor.Prezime,
                        p.Pacijent.Mail
                    })
                    .ToListAsync();

                if(spoj.Count==0)
                {
                    return NoContent();
                }

                return Ok(spoj);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("ObrisiSpoj/{id}")]
        [HttpDelete]
        public async Task<ActionResult> ObrisiSpoj(int id)
        {
            if(id<=0)
            {
                return BadRequest("Ne valja id");
            }
            try
            {
                var temp = await Context.Spojevi.FindAsync(id);
                Context.Spojevi.Remove(temp);
                await Context.SaveChangesAsync();
                return Ok("Uspesno obrisan spoj");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("Proveri/{idDoktora}/{datum}")]
        [HttpGet]
        public async Task<ActionResult> Proveri(int idDoktora,string datum)
        {
            if(idDoktora<=0)
            {
                BadRequest("Nevalidan id");
            }
            if(string.IsNullOrWhiteSpace(datum))
            {
                BadRequest("Nevalidan datum");
            }
            string te = datum+":00:00";
            DateTime vr = DateTime.Parse(te);
            var temp = await Context.Spojevi
            .Include(p=>p.Doktor).Where(p=>p.Doktor.ID==idDoktora)
            .Where(p=>p.Datum==vr).FirstOrDefaultAsync();
            if(temp==null)
            {
                return Ok("Slobodan");
            }
            else
            {
                return NoContent();
            }
        }

    
    
        [Route("Datumi/{idDoktora}")]
        [HttpGet]
        public async Task<ActionResult> Datum(int idDoktora)
            {
                if(idDoktora<=0)
                {
                    BadRequest("Nevalidan id");
                }
                var temp = await Context.Spojevi
                .Include(p=>p.Doktor).Where(p=>p.Doktor.ID==idDoktora).Select(p=>new{
                    p.Datum
                }).ToListAsync();
                return Ok(temp);
            }

    [NonAction]
    public bool IsValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}}
