using ELCLite.Identity.Data.Entities;

namespace ELCLite.Identity.Data.Seeds
{
    public static class UserSeed
    {
        public static List<User> GetGlobalAdminUsers()
        {
            var users = new List<User>();

            users.Add(new User("peter.jones@loyaltylogistix.com", "Peter", "Jones", "en-US", true));
            users.Add(new User("peter_jones_glass@hotmail.com", "Peter A", "Jones", "en-US", true));

            return users;
        }
    }
}
