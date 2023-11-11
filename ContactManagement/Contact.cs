using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagement
{
    public static class ContactDB
    {
        public static List<Contact> contacts = new List<Contact>();
    }
    public class Contact
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
    public enum UserRole
    {
        Admin,
        User
    }
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
    }
   
    public class UserRepository : IRepository<User>
    {
        private List<User> users = new List<User>();
        public void Add(User user)
        {
            users.Add(user);
        }

        public void Delete(User user)
        {
            User currentUser = users.FirstOrDefault(c => c.Id == user.Id || c.Username == user.Username);
            users.Remove(currentUser);
        }

        public IEnumerable<User> GetAll()
        {
            return users;
        }

        public User GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(User contact)
        {
            throw new NotImplementedException();
        }
    }
    public class ContactRepository : IRepository<Contact>
    {
        public void Add(Contact contact)
        {
            ContactDB.contacts.Add(contact);
        }
        public void Update(Contact contact)
        {
            Contact contactToUpdate = ContactDB.contacts.FirstOrDefault(c => c.Name.Equals(contact.Name, StringComparison.OrdinalIgnoreCase));

            if (contactToUpdate.Name != null)
            {
                contactToUpdate.Name = contact.Name;
            }
            if (contactToUpdate.PhoneNumber != null)
            {
                contactToUpdate.PhoneNumber = contact.PhoneNumber;
            }
            if (contactToUpdate.Email != null)
            {
                contactToUpdate.Email = contact.Email;
            }
            if (contactToUpdate.Address != null)
            {
                contactToUpdate.Address = contact.Address;
            }
        }
        public void Delete(Contact contact)
        {
            Contact contactToDelete = ContactDB.contacts.FirstOrDefault(c => c.Name.Equals(contact.Name, StringComparison.OrdinalIgnoreCase) || c.PhoneNumber.Equals(contact.PhoneNumber, StringComparison.OrdinalIgnoreCase));
            ContactDB.contacts.Remove(contactToDelete);
        }

        public IEnumerable<Contact> GetAll()
        {
            return ContactDB.contacts;
        }

        public Contact GetByName(string name)
        {
            Contact contact = ContactDB.contacts.FirstOrDefault(c => c.Name == name);
            return contact;
        }
    }
}
