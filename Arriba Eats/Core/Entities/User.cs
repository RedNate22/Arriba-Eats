using System;

namespace Entities
{
    public abstract class User
    {
        public string? Name { get; private set; }
        public int Age { get; private set; }
        public string? Email { get; private set; }
        public string? Mobile { get; private set; }
        public string? Password { get; private set; }
        public string? Location { get; private set; }
        
        protected User(string name, int age, string email, string mobile, string password, string location)
        {
            Name = name;
            Age = age;
            Email = email;
            Mobile = mobile;
            Password = password;
            Location = location;
        }
    }
    
}