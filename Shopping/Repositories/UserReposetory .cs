using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Shopping.Context;
using Shopping.Models.Entities;

namespace Shopping.Repositories
{
    public class UserReposetory(MyDBContext _db) : IUserReposetory
    {
        public async Task<User?> AuthenticateUser(string username, string password)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
    }
}
