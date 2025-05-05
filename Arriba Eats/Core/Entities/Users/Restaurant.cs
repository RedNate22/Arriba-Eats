using System;

namespace Entities
{
    public class Restaurant : User
    {
        public Restaurant(string name, int age, string email, string mobile, string password, string location) 
            : base(name, age, email, mobile, password, location)
        {
            //
        }
    }
}