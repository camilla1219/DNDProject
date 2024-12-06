using DNDBlazorApp.Models.Entities;
using DNDBlazorApp.Data;

namespace DNDBlazorApp.Services
{
    public class UserService
    {
        private readonly AppDbContext context;

        public UserService(AppDbContext context)
        {
            this.context = context;
        }

        public bool SaveUser(UserAccount user)
        {
            bool isExist = context.UserAccounts.Any(x => x.Email == user.Email);
            if (!isExist)
            {
                context.UserAccounts.Add(user);
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public  UserAccount? Verify(string email, string password)
        {
            return context.UserAccounts.FirstOrDefault(x => x.Email.ToLower() == email.ToLower()
                    && x.Password == password);
        }
    }
}
