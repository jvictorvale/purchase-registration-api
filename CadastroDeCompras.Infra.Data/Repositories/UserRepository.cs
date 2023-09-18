using CadastroDeCompras.Domain.Entities;
using CadastroDeCompras.Domain.Repositories;
using CadastroDeCompras.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace CadastroDeCompras.Infra.Data.Repositories
{

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
    }
}