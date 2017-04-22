using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public User(int id, string name, string email, string password, bool isAdmin)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            IsAdmin = isAdmin;
        }

        public override string ToString()
        {
            return Id + ")" + Name + " email: " + Email + " pass: " + Password + " admin: " + IsAdmin;
        }
    }
}
