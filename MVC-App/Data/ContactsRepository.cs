using Dapper;
using MVC_App.Models;
using System.Data;

namespace MVC_App.Data
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly IDbConnection _dbconnection;
        public ContactsRepository(IDbConnection connection)
        {
            _dbconnection = connection;
        }
        
        public async Task Delete(int id)
        {
            var consulta = @"DELETE FROM Contacts 
                                   where Id = @id";

            await _dbconnection.ExecuteAsync(consulta, new { id });
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
           var consulta = @"SELECT Id, FirstName, LastName, Phone, Address
                                   FROM Contacts
                                   ORDER BY FirstName, LastName";

            return await _dbconnection.QueryAsync<Contact>(consulta, new {});
        }

        public async Task<Contact?> GetDetailsById(int id)
        {
            var consulta = @"SELECT Id, FirstName, LastName, Phone, Address 
                                   FROM Contacts
                                   WHERE Id = @Id";

            return await _dbconnection.QueryFirstOrDefaultAsync<Contact>(consulta, new { Id = id });
        }

        public async Task Insert(Contact contact)
        {
            var consulta = @"INSERT INTO Contacts (FirstName,LastName,Phone,Address)
                             VALUES (@Firstname,@LastName,@Phone,@Address)";

             await _dbconnection.ExecuteAsync(consulta, new {
                contact.FirstName,
                contact.LastName,
                contact.Phone,
                contact.Address });
        }

        public async Task Update(Contact contact)
        {
            var consulta = @"UPDATE Contacts 
                             SET FirstName = @Firstname,
                                 LastName = @Lastname,
                                 Phone = @Phone,
                                 Address = @Address
                             WHERE Id = @Id";

             await _dbconnection.ExecuteAsync(consulta, new { 
                 contact.FirstName, 
                 contact.LastName,
                 contact.Phone, 
                 contact.Address,
                 contact.Id
             });
        }
    }
}
