using Microsoft.AspNetCore.Mvc;
using ExpenseTracker.Application.UseCases.ContactUseCases;
using ExpenseTracker.Core.Entities;
using System;

namespace MyExpenseTrackerWithCookies.Controllers
{
    public class ContactController : Controller
    {
        private readonly AddContactUseCase _addContactUseCase;
        private readonly GetAllContactsUseCase _getAllContactsUseCase;

        public ContactController(AddContactUseCase addContactUseCase, GetAllContactsUseCase getAllContactsUseCase)
        {
            _addContactUseCase = addContactUseCase;
            _getAllContactsUseCase = getAllContactsUseCase;
        }

        public IActionResult Index()
        {
            var listOfAllContacts = _getAllContactsUseCase.Execute();
            return View(listOfAllContacts);
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View(new ContactEntity());
        }

        [HttpPost]
        public IActionResult ContactUs(ContactEntity contact)
        {
            if (ModelState.IsValid)
            {
                contact.DateSubmitted = DateTime.Now;
                _addContactUseCase.Execute(contact);
                TempData["SuccessMessage"] = "Your message has been sent to the SpendSmart Team";
                return RedirectToAction("ContactUs");
            }

            return View(contact);
        }
    }
}
