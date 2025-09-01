using MVC_App.Models;
using System.Diagnostics.Contracts;

namespace MVC_App.Data
{
    public interface IContactsRepository
    {
        Task<IEnumerable<Contact>> GetAll();
      
        Task<Contact?> GetDetailsById(int id);
        
        Task Insert(Contact contact);
        
        Task Update(Contact contact);
        
        Task Delete(int id);
    }
}
