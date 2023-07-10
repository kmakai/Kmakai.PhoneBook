﻿
using Kmakai.PhoneBook.Controllers;
using Kmakai.PhoneBook.Models;
using Spectre.Console;

namespace Kmakai.PhoneBook.Services;

public class ContactService
{

    public static void CreateContact()
    {
        Contact contact = new Contact();

        contact.Name = AnsiConsole.Ask<string>("Enter contact name:");
        contact.PhoneNumber = AnsiConsole.Ask<string>("Enter contact phone number:");
        contact.Email = AnsiConsole.Ask<string>("Enter contact email:");
        contact.CategoryId = CategoryController.GetCategoryIdByName();

        ContactController.AddContact(contact);

        AnsiConsole.MarkupLine($"[bold green]Contact {contact.Name} added successfully![/]");
        AnsiConsole.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void GetContact()
    {
        var contacts = ContactController.GetContacts();
        var contactNames = contacts.Select(x => x.Name).ToArray();
        var contactOption = AnsiConsole.Prompt<string>(new SelectionPrompt<string>()
                               .Title("Select contact")
                               .PageSize(10)
                               .MoreChoicesText("[grey](Move up and down to reveal more contacts)[/]")
                               .AddChoices(contactNames));

        var contact = contacts.SingleOrDefault(x => x.Name == contactOption);

        if (contact == null)
        {
            AnsiConsole.MarkupLine("[bold red]Contact not found![/]");
            return;
        }

        var table = new Table();

        table.AddColumn("Name");
        table.AddColumn("Phone Number");
        table.AddColumn("Email");

        table.AddRow(contact.Name, contact.PhoneNumber, contact.Email);

        AnsiConsole.Write(table);
    }

    public static void UpdateContact()
    {
        var contacts = ContactController.GetContacts();
        var contactNames = contacts.Select(x => x.Name).ToArray();
        var contactOption = AnsiConsole.Prompt<string>(new SelectionPrompt<string>()
                                .Title("Select contact to update")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more contacts)[/]")
                                .AddChoices(contactNames));

        var contact = contacts.SingleOrDefault(x => x.Name == contactOption);
        if (contact == null)
        {
            AnsiConsole.MarkupLine("[bold red]Contact not found![/]");
            return;
        }

        contact.Name = AnsiConsole.Confirm("Update contact name?") ? AnsiConsole.Ask<string>("Enter new contact name:") : contact.Name;
        contact.PhoneNumber = AnsiConsole.Confirm("Update contact phone number?") ? AnsiConsole.Ask<string>("Enter new contact phone number:") : contact.PhoneNumber;
        contact.Email = AnsiConsole.Confirm("Update contact email?") ? AnsiConsole.Ask<string>("Enter new contact email:") : contact.Email;
        contact.CategoryId = AnsiConsole.Confirm("Update contact category?") ? CategoryController.GetCategoryIdByName() : contact.CategoryId;

        ContactController.UpdateContact(contact);

        AnsiConsole.MarkupLine($"[bold green]Contact {contact.Name} updated successfully![/]");

        AnsiConsole.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void DeleteContact()
    {
        var contacts = ContactController.GetContacts();
        var contactNames = contacts.Select(x => x.Name).ToArray();
        var contactOption = AnsiConsole.Prompt<string>(new SelectionPrompt<string>()
                                .Title("Select contact to delete")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more contacts)[/]")
                                .AddChoices(contactNames));

        var contact = contacts.SingleOrDefault(x => x.Name == contactOption);
        if (contact == null)
        {
            AnsiConsole.MarkupLine("[bold red]Contact not found![/]");
            return;
        }

        ContactController.DeleteContactById(contact.Id);

        AnsiConsole.MarkupLine($"[bold green]Contact {contact.Name} deleted successfully![/]");

        AnsiConsole.WriteLine("Press any key to continue");
        Console.ReadKey();
    }

    public static void GetContacts()
    {
        var contacts = ContactController.GetContacts();

        var table = new Table();

        table.AddColumn("Name");
        table.AddColumn("Phone Number");
        table.AddColumn("Email");

        foreach (var contact in contacts)
        {
            table.AddRow(contact.Name, contact.PhoneNumber, contact.Email);
        }

        AnsiConsole.Write(table);
        
    }
}