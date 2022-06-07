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
using System.Web.Mvc;
using SearchAPI.Helper;
using SearchAPI.Models;

namespace SearchAPI.Controllers
{
    public class AllUsers_ResultController : ApiController
    {
        private searchDBEntities db = new searchDBEntities();

        // GET: api/AllUsers_Result
        public IQueryable<AllUsers_Result> GetAllUsers_Result()
        {
            return db.AllUsers_Result;
        }

        // GET: api/AllUsers_Result/5
        [ResponseType(typeof(UserInfo))]
        public IHttpActionResult GetAllUsers_Result(string first_name, string last_name, string address, string email, string dateofbirth, int pageSize = 5, int pageNumber = 1)
        {
            DateTime datetime = new  DateTime();
            try
            {
                datetime = DateTime.Parse(dateofbirth);
            }
            catch (Exception e)
            {

            }
                
           
            List<UserInfo> allUsers_Result = db.UserInfoes.Where(x=> x.first_name.Equals(first_name) || x.last_name.Equals(last_name) || x.address.Equals(address)|| x.email.Equals(email) || DateTime.Compare(x.date_of_birth,datetime)==0).ToList();
            var pagedData = Pagination.PagedResult(allUsers_Result, pageNumber, pageSize);
            return Json(pagedData);

            //if (allUsers_Result == null)
            //{
            //    return NotFound();
            //}

            //return Ok(allUsers_Result);
        }

       
        // PUT: api/AllUsers_Result/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAllUsers_Result(int id, AllUsers_Result allUsers_Result)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != allUsers_Result.id)
            {
                return BadRequest();
            }

            db.Entry(allUsers_Result).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllUsers_ResultExists(id))
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

        // POST: api/AllUsers_Result
       
        // DELETE: api/AllUsers_Result/5
        [ResponseType(typeof(AllUsers_Result))]
        public IHttpActionResult DeleteAllUsers_Result(int id)
        {
            AllUsers_Result allUsers_Result = db.AllUsers_Result.Find(id);
            if (allUsers_Result == null)
            {
                return NotFound();
            }

            db.AllUsers_Result.Remove(allUsers_Result);
            db.SaveChanges();

            return Ok(allUsers_Result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AllUsers_ResultExists(int id)
        {
            return db.AllUsers_Result.Count(e => e.id == id) > 0;
        }
    }
}