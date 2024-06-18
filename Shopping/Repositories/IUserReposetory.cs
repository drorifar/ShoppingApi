using Shopping.Models.Entities;

namespace Shopping.Repositories
{
    public interface IUserReposetory
    {
        Task<User?> AuthenticateUser(string username, string password);
    }
}
