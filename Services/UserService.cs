using UserManagementAPI.Models;

namespace UserManagementAPI.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }

    public class UserService : IUserService
    {
        private readonly List<User> _users = new();

        public UserService()
        {
            // Initialize the default list with 5 users
            _users.AddRange(new List<User>
            {
                new User { Id = 1, FirstName = "Frodo", LastName = "Baggins", Email = "frodo.baggins@lotr.com" },
                new User { Id = 2, FirstName = "Aragorn", LastName = "Elessar", Email = "aragorn.elessar@lotr.com" },
                new User { Id = 3, FirstName = "Legolas", LastName = "Greenleaf", Email = "legolas.greenleaf@lotr.com" },
                new User { Id = 4, FirstName = "Gandalf", LastName = "The Grey", Email = "gandalf.grey@lotr.com" },
                new User { Id = 5, FirstName = "Samwise", LastName = "Gamgee", Email = "samwise.gamgee@lotr.com" }
            });
        }


        public IEnumerable<User> GetAllUsers() => _users;

        public User GetUserById(int id) => _users.FirstOrDefault(u => u.Id == id);

        public void AddUser(User user)
        {
            user.Id = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            _users.Add(user);
        }

        public void UpdateUser(User user)
        {
            var existingUser = GetUserById(user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
            }
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                _users.Remove(user);
            }
        }
    }
}
