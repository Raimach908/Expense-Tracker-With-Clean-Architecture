using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;

namespace ExpenseTracker.Application.UseCases.ContactUseCases
{
    public class AddContactUseCase
    {
        private readonly IContactRepository _contactRepository;

        public AddContactUseCase(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public void Execute(ContactEntity contact)
        {
            _contactRepository.AddContact(contact);
        }
    }
}
