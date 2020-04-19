using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Models;

namespace RealEstateApp.DataAccessLayer
{
    public class ContactsRepository
    {
        private readonly RealEstate_DBContext _db;

        /// <summary>
        /// Constructor accepting a Database Context
        /// </summary>
        /// <param name="db">Database Context</param>
        public ContactsRepository(RealEstate_DBContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Determines if item exists given passed ID parameter
        /// </summary>
        /// <param name="id">ID field of database</param>
        /// <returns>true / false</returns>
        public bool ContactExists(long id)
        {
            return _db.Contacts.Any(c => c.ContactsId == id);
        }

        /// <summary>
        /// Create a new Customers Entry
        /// </summary>
        /// <param name="contact">Customer Object</param>
        /// <returns>Customer Added</returns>
        public Contacts CreateCustomer(Contacts contact)
        {
            _db.Contacts.Add(contact);
            _db.SaveChanges();

            return contact;
        }

        /// <summary>
        /// Get all Customer Items
        /// </summary>
        /// <returns>List of Customer Objects</returns>
        public List<Contacts> GetAllContacts()
        {
            return _db.Contacts.AsNoTracking().ToList();
        }

        /// <summary>
        /// Get a Customer based on passed ID
        /// </summary>
        /// <param name="id">ID of item sought out</param>
        /// <returns>Returns Customers Object</returns>
        public Contacts GetContact(long id)
        {
            return _db.Contacts.FirstOrDefault(c => c.ContactsId == id);
        }

        /// <summary>
        /// Get List of Customer Objects Customer First Names
        /// </summary>
        /// <param name="firstNames">Names</param>
        /// <returns>List of Customers</returns>
        public List<Contacts> GetAllContacsbyFirstNames(String firstNames)
        {
            //Detect non-existing vendor
            return _db.Contacts.AsNoTracking().Where(c => c.ContactNames == firstNames).ToList();
        }

        /// <summary>
        /// Get List of Customer Objects Customer Last Names
        /// </summary>
        /// <param name="lNames">Names</param>
        /// <returns>List of Customers</returns>
        public List<Contacts> GetAllCustomersbyLastNames(String lNames)
        {
            //Detect non-existing vendor
            return _db.Contacts.AsNoTracking().Where(c => c.ContactLastNames == lNames).ToList();
        }

        /// <summary>
        /// Deletes Customers
        /// </summary>
        /// <param name="id">ID of Customer to Delete</param>
        public void DeleteCustomer(long id)
        {
            Contacts cont = GetContact(id);
            if (cont != null)
            {
                _db.Contacts.Remove(cont);
                _db.SaveChanges();
            }
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="id">ID of original Customer</param>
        /// <param name="updatedcont">Updated Bill</param>
        public void UpdateContact(long id, Contacts updatedcont)
        {
            Contacts cont = GetContact(id);
            if (cont == null)
                throw new Exception("Contact not found!");

            cont.ContactNames = updatedcont.ContactNames;
            cont.ContactLastNames = updatedcont.AlternateNumber;
            cont.IdNumber = updatedcont.IdNumber;
            cont.PhoneNumber = updatedcont.PhoneNumber;
            cont.AlternateNumber = updatedcont.AlternateNumber;
            cont.EmailAddress = updatedcont.EmailAddress;
            cont.ContactType = updatedcont.ContactType;
            cont.FkCompaniesId = updatedcont.FkCompaniesId;            

            _db.SaveChanges();
        }
    }
}
