using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Models;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsApiController : ControllerBase
    {
        private readonly RealEstate_DBContext _context;

        public ContactsApiController(RealEstate_DBContext context)
        {
            _context = context;
        }

        // GET: api/ContactsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacts>>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        // GET: api/ContactsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contacts>> GetContacts(int id)
        {
            var contacts = await _context.Contacts.FindAsync(id);

            if (contacts == null)
            {
                return NotFound();
            }

            return contacts;
        }

        // PUT: api/ContactsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacts(int id, Contacts contacts)
        {
            if (id != contacts.ContactsId)
            {
                return BadRequest();
            }

            _context.Entry(contacts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ContactsApi
        [HttpPost]
        public async Task<ActionResult<Contacts>> PostContacts(Contacts contacts)
        {
            _context.Contacts.Add(contacts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContacts", new { id = contacts.ContactsId }, contacts);
        }

        // DELETE: api/ContactsApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contacts>> DeleteContacts(int id)
        {
            var contacts = await _context.Contacts.FindAsync(id);
            if (contacts == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contacts);
            await _context.SaveChangesAsync();

            return contacts;
        }

        private bool ContactsExists(int id)
        {
            return _context.Contacts.Any(e => e.ContactsId == id);
        }
    }
}
