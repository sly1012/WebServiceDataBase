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
using WebServiceDemo;

namespace WebServiceDemo.Controllers
{
    public class DescriptionsController : ApiController
    {
        private HotelContext db = new HotelContext();

        // GET: api/Descriptions
        public IQueryable<Description> GetDescription()
        {
            return db.Description;
        }

        // GET: api/Descriptions/5
        [ResponseType(typeof(Description))]
        public IHttpActionResult GetDescription(int id)
        {
            Description description = db.Description.Find(id);
            if (description == null)
            {
                return NotFound();
            }

            return Ok(description);
        }

        // PUT: api/Descriptions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDescription(int id, Description description)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != description.Id)
            {
                return BadRequest();
            }

            db.Entry(description).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DescriptionExists(id))
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

        // POST: api/Descriptions
        [ResponseType(typeof(Description))]
        public IHttpActionResult PostDescription(Description description)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Description.Add(description);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DescriptionExists(description.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = description.Id }, description);
        }

        // DELETE: api/Descriptions/5
        [ResponseType(typeof(Description))]
        public IHttpActionResult DeleteDescription(int id)
        {
            Description description = db.Description.Find(id);
            if (description == null)
            {
                return NotFound();
            }

            db.Description.Remove(description);
            db.SaveChanges();

            return Ok(description);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DescriptionExists(int id)
        {
            return db.Description.Count(e => e.Id == id) > 0;
        }
    }
}