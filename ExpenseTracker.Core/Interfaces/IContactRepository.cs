using ExpenseTracker.Core.Entities;
namespace ExpenseTracker.Core.Interfaces
{
    public interface IContactRepository
    {
        void AddContact(ContactEntity contact);
        IEnumerable<ContactEntity> GetContacts();
    }
}
