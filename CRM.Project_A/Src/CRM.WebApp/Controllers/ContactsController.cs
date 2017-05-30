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
using EntityLibrary;
using CRM.WebApi.Models;
using System.Threading.Tasks;
using CRM.WebApp.Infrastructure;
using CRM.WebApp.Models;

namespace CRM.WebApp.Controllers
{
    public class ContactsController : ApiController
    {
        //private DataBaseCRMEntityes db = new DataBaseCRMEntityes();
        private ApplicationManager manager = new ApplicationManager();

        // GET: api/Contacts
        public async Task<List<ContactResponseModel>> GetContacts()
        {
            return await manager.GetAllContacts();
        }

        // GET: api/Contacts/paje
        //[ResponseType(typeof(Contact))]
        //public async Task<IHttpActionResult> GetContact(int start, int numberRows, bool flag)
        //{
        //    var contact = await manager.GetContactPage(start, numberRows, flag);
        //    if (contact == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(contact);
        //}

        // GET: api/Contacts/guid
        [ResponseType(typeof(ApiContactsModel))]
        public async Task<IHttpActionResult> GetContactGuid(Guid id)
        {
            var contact = await manager.GetContactByGuid(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }


        //GET: api/Contacts/Npaje
        [Route("api/Contact/pages")]
        public async Task<int> GetContactsPageCount()
        {
            return await manager.GetContactsPageCounter();
        }

        //// PUT: api/Contacts/5
        //[ResponseType(typeof(void))]
        //public async Task<bool> PutContact(ViewContact contact)
        //{
        //    Contact dbContactToUpdate = await GetContactGuid(contact.GuID);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    int id = contact.ContactId;
        //    Contact ContactsUpdate = await manager.UpdateContact(contact);

        //    if (ContactsUpdate == null)
        //    {
        //        return BadRequest(modelState);
        //    }

        //    try
        //    {
        //        manager.SaveDb();
        //    }

        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!(await ContactExists(id)))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);

        //}
        //// PUT: api/Contacts/5
        //[ResponseType(typeof(void))]
        //[HttpPut]
        //public async Task<IHttpActionResult> PutContact(string guid, [FromBody] ContactRequestModel contact)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (await manager.UpdateContact(guid, contact))
        //        return StatusCode(HttpStatusCode.NoContent);

        //    return NotFound();

        //}
        // POST: api/Contacts
        [ResponseType(typeof(ContactRequestModel))]
        public async Task<IHttpActionResult> PostContact(ContactRequestModel contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            await manager.AddContact(contact);

            return CreatedAtRoute("DefaultApi", new { }, contact);
        }

        // DELETE: api/Contacts/5
        [ResponseType(typeof(ContactResponseModel))]
        public async Task<IHttpActionResult> DeleteContact(string guid)
        {
            var contact = await manager.RemoveContact(guid);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        //// POST: api/Contacts
        //[Route("api/Contacts/upload")]
        //[ResponseType(typeof(Contact))]
        //public IHttpActionResult PostContactUpload([FromBody]byte[] array)
        //{
        //   // return CreatedAtRoute("DefaultApi", new { id = contact.ContactId }, contact);
        //}
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                manager.Dispose();
            }
            base.Dispose(disposing);
        }

        private async Task<bool> ContactExists(Guid id)
        {
            return await manager.ContactExistsAsync(id);
        }
    }
}