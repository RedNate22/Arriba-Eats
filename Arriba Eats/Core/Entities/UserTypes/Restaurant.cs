using System;

namespace Entities
{
    public class Restaurant : User
    {
        public Restaurant(string name, int age, string email, string mobile, string password) 
            : base(name, age, email, mobile, password)
        {
            //
        }
    }
}