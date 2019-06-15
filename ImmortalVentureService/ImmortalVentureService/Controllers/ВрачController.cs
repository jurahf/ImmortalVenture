using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ImmortalVentureService.Model;

namespace ImmortalVentureService.Controllers
{
    public class DoctorController : ApiController
    {
        private HakatonModelContainer db = new HakatonModelContainer();

        // GET: api/Врач
        public IQueryable<Врач> GetВрачSet()
        {
            return db.ВрачSet;
        }

        // GET: поиск по id api/Врач/getВрач?id=1
        [HttpGet]
        [ActionName("GetDoctor")]
        [ResponseType(typeof(Врач))]
        public IHttpActionResult GetВрач(int id)
        {
            Врач врач = db.ВрачSet.Find(id);
            if (врач == null)
            {
                return NotFound();
            }

            return Ok(врач);
        }

        // PUT: изменение api/Врач/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutВрач(int id, Врач врач)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != врач.Id)
            {
                return BadRequest();
            }

            db.Entry(врач).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ВрачExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: добавление api/Врач
        [HttpPost]
        [ResponseType(typeof(Врач))]
        public IHttpActionResult PostВрач(Врач врач)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ВрачSet.Add(врач);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = врач.Id }, врач);
        }

        // DELETE: удаление api/Врач/5
        [ResponseType(typeof(Врач))]
        [HttpDelete]
        public IHttpActionResult DeleteВрач(int id)
        {
            Врач врач = db.ВрачSet.Find(id);
            if (врач == null)
            {
                return NotFound();
            }

            db.ВрачSet.Remove(врач);
            db.SaveChanges();

            return Ok(врач);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ВрачExists(int id)
        {
            return db.ВрачSet.Count(e => e.Id == id) > 0;
        }
    }
}