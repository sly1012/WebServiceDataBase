﻿using System;
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
    public class DemoGuestsController : ApiController
    {
        private HotelContext db = new HotelContext();

        // GET: api/DemoGuests
        public IQueryable<DemoGuest> GetDemoGuest()
        {
            return db.DemoGuest;
        }

        // GET: api/DemoGuests/5
        [ResponseType(typeof(DemoGuest))]
        public IHttpActionResult GetDemoGuest(int id)
        {
            DemoGuest demoGuest = db.DemoGuest.Find(id);
            if (demoGuest == null)
            {
                return NotFound();
            }

            return Ok(demoGuest);
        }

        // PUT: api/DemoGuests/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDemoGuest(int id, DemoGuest demoGuest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != demoGuest.Guest_No)
            {
                return BadRequest();
            }

            db.Entry(demoGuest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DemoGuestExists(id))
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

        // POST: api/DemoGuests
        [ResponseType(typeof(DemoGuest))]
        public IHttpActionResult PostDemoGuest(DemoGuest demoGuest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DemoGuest.Add(demoGuest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DemoGuestExists(demoGuest.Guest_No))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = demoGuest.Guest_No }, demoGuest);
        }

        // DELETE: api/DemoGuests/5
        [ResponseType(typeof(DemoGuest))]
        public IHttpActionResult DeleteDemoGuest(int id)
        {
            DemoGuest demoGuest = db.DemoGuest.Find(id);
            if (demoGuest == null)
            {
                return NotFound();
            }

            db.DemoGuest.Remove(demoGuest);
            db.SaveChanges();

            return Ok(demoGuest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DemoGuestExists(int id)
        {
            return db.DemoGuest.Count(e => e.Guest_No == id) > 0;
        }
    }
}