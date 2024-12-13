using ExpenseTracker.Core.Entities;
using ExpenseTracker.Core.Interfaces;
using System.Collections.Generic;

namespace ExpenseTracker.Application.UseCases.ContactUseCases
{
    public class GetAllContactsUseCase
    {
        private readonly IContactRepository _contactRepository;

        public GetAllContactsUseCase(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IEnumerable<ContactEntity> Execute()
        {
            return _contactRepository.GetContacts();
        }
    }
}
