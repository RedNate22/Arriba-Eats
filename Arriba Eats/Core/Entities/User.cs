using System;

namespace Entities
{
    public abstract class User
    {
        private string? _name;
        public string? Name 
        { 
            get { return _name; } 
            set { _name = value ?? ""; }
        }

        private int _age;
        public int Age 
        { 
            get { return _age; } 
            set { _age = value; } 
        }

        private string? _email;
        public string? Email 
        { 
            get { return _email; } 
            set { _email = value; }
        }

        public void CreateUser(string name, int age, string email)
        {
            // TODO create user from registration menu and then
            //list.add(Name, Age, Email etc....)
        }
    }
}