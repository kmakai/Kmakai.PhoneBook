using Kmakai.PhoneBook.Data;
using Kmakai.PhoneBook.Models;

namespace Kmakai.PhoneBook.Controllers;

public class ContactController
{
    public static void AddContact(Contact contact)
    {
        using var db = new AppDbContext();
        db.Contacts.Add(contact);
        db.SaveChanges();
    }

    public static Contact GetContact(int id)
    {
        using var db = new AppDbContext();
        var contact = db.Contacts.SingleOrDefault(x => x.Id == id);      
        return contact;
    }

    public static List<Contact> GetContacts()
    {
        using var db = new AppDbContext();
        return db.Contacts.ToList();
    }

    public static void UpdateContact(Contact contact)
    {
        using var db = new AppDbContext();
        db.Contacts.Update(contact);
        db.SaveChanges();
    }

    public static void DeleteContact(int id)
    {
        using var db = new AppDbContext();
        var contact = db.Contacts.SingleOrDefault(x => x.Id == id);
        db.Contacts.Remove(contact);
        db.SaveChanges();
    }
}
