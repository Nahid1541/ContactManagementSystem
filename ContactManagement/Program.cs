using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ContactManagement
{
    internal class Program
    {
        static User currentUser = null;
        static IRepository<User> userRepository = new UserRepository();
        static void Main(string[] args)
        {
            ContactRepository repository = new ContactRepository();
            while (true)
            {
                Console.Clear();
                if (currentUser == null)
                {
                    Console.WriteLine("===== Contact Management System =====");
                    Console.WriteLine("1. Login");
                    Console.WriteLine("2. Register");
                    Console.WriteLine("3. Exit");
                    Console.WriteLine("===============================");
                }
                else
                {
                    Console.WriteLine($"===== Contact Management System - Welcome, {currentUser.Username}! =====");
                    Console.WriteLine("1. Add Contact");
                    Console.WriteLine("2. Update Contact");
                    Console.WriteLine("3. Delete Contact");
                    Console.WriteLine("4. List All Contacts");
                    Console.WriteLine("5. Search Contacts");
                    Console.WriteLine("6. Exit");
                    Console.WriteLine("==========================================");
                }

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                if (currentUser == null)
                {
                    switch (choice)
                    {
                        case "1":
                            Login();
                            break;

                        case "2":
                            Register();
                            break;

                        case "3":
                            return;

                        default:
                            Console.WriteLine("Invalid choice. Press Enter to continue.");
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    switch (choice)
                    {
                        case "1":
                            {
                                // Add Contact
                                Console.WriteLine("Adding a new contact...");
                                Console.Write("Enter name: ");
                                string Name = Console.ReadLine();
                                Console.Write("Enter phone number: ");
                                string PhoneNumber = Console.ReadLine();
                                Console.Write("Enter email: ");
                                string Email = Console.ReadLine();
                                Console.Write("Enter address: ");
                                string Address = Console.ReadLine();

                                Contact contact = new Contact
                                {
                                    Name = Name,
                                    PhoneNumber = PhoneNumber,
                                    Email = Email,
                                    Address = Address
                                };
                                repository.Add(contact);
                                Console.WriteLine("Contact added successfully.");
                                Console.WriteLine("Press Enter to continue.");
                                Console.ReadLine();
                                break;
                            }
                        case "2":
                            {
                                // Update Contact
                                Console.WriteLine("Updating a contact...");
                                Console.Write("Enter the name of the contact to update: ");
                                string nameToUpdate = Console.ReadLine();
                                var contact = repository.GetByName(nameToUpdate);

                                if (contact == null)
                                {
                                    Console.WriteLine("Contact not found.");
                                    return;
                                }
                                else
                                {
                                    Console.Write("Enter new phone number (leave empty to keep current): ");
                                    string newPhoneNumber = Console.ReadLine();

                                    Console.Write("Enter new email (leave empty to keep current): ");
                                    string newEmail = Console.ReadLine();

                                    Console.Write("Enter new address (leave empty to keep current): ");
                                    string newAddress = Console.ReadLine();

                                    Contact updateContact = new Contact
                                    {
                                        Name = nameToUpdate,
                                        PhoneNumber = newPhoneNumber,
                                        Email = newEmail,
                                        Address = newAddress
                                    };

                                    repository.Update(updateContact);
                                    Console.WriteLine("Contact updated successfully.");
                                }
                                Console.WriteLine("Press Enter to continue.");
                                Console.ReadLine();
                                break;
                            }
                        case "3":
                            {
                                // Delete Contact
                                Console.WriteLine("Deleting a contact...");
                                Console.Write("Enter the name of the contact to delete: ");
                                string nameToDelete = Console.ReadLine();
                                var deleteContact = repository.GetByName(nameToDelete);
                                if (deleteContact == null)
                                {
                                    Console.WriteLine("Contact not found.");
                                    return;
                                }
                                else
                                {
                                    repository.Delete(deleteContact);
                                    Console.WriteLine("Contact deleted successfully.");
                                }
                                Console.WriteLine("Press Enter to continue.");
                                Console.ReadLine();
                                break;
                            }
                        case "4":
                            {
                                // List All Contacts
                                ContactRepository contactRepository = new ContactRepository();
                                Console.WriteLine("Listing all contacts...");
                                var contactList = contactRepository.GetAll();
                                if (contactList.Count() > 0)
                                {
                                    foreach (var contact in contactList)
                                    {
                                        Console.WriteLine($"Name: {contact.Name}, Phone: {contact.PhoneNumber}, Email: {contact.Email}, Address: {contact.Address}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No contacts available.");
                                }
                                Console.WriteLine("Press Enter to continue.");
                                Console.ReadLine();
                                break;
                            }
                        case "5":
                            {
                                // Search Contacts
                                Console.WriteLine("Searching for contacts...");
                                Console.Write("Enter search keyword: ");
                                string searchKeyword = Console.ReadLine();
                                var searchResult = ContactDB.contacts.Where(c => c.Name.Equals(searchKeyword, StringComparison.OrdinalIgnoreCase) ||
                                                                            c.PhoneNumber.Equals(searchKeyword, StringComparison.OrdinalIgnoreCase) ||                                                           
                                                                            c.Address.Equals(searchKeyword, StringComparison.OrdinalIgnoreCase)).ToList();
                                if (searchResult.Count > 0)
                                {
                                    Console.WriteLine("Search Results: ");
                                    foreach (var contact in searchResult)
                                    {
                                        Console.WriteLine($"Name: {contact.Name}, Phone: {contact.PhoneNumber}, Email: {contact.Email}, Address: {contact.Address}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No matching contacts found.");
                                }
                                Console.WriteLine("Press Enter to continue.");
                                Console.ReadLine();
                                break;
                            }
                        case "6":
                            {
                                // Exit the program
                                Console.WriteLine("Exiting...");
                                return;
                            }
                        default:
                            Console.WriteLine("Invalid choice. Press Enter to continue.");
                            Console.ReadLine();
                            break;
                    }
                }
            }
        }
        // User Login Implementation
        static void Login()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            User user = userRepository.GetAll().FirstOrDefault(u => u.Username == username && u.Password == password);

            if (user != null)
            {
                currentUser = user;
                Console.WriteLine($"Welcome, {user.Username}!");
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
        // User Registration Implementation
        static void Register()
        {
            Console.Write("Enter a new username: ");
            string username = Console.ReadLine();
            Console.Write("Enter a new password: ");
            string password = Console.ReadLine();

            User newUser = new User
            {
                Username = username,
                Password = password,
                Role = UserRole.User
            };
            userRepository.Add(newUser);

            Console.WriteLine("Registration successful. Please log in.");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
    }
}
