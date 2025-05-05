using System;

namespace Entities
{
    public class Deliverer : User
    {
        public Deliverer(string name, int age, string email, string mobile, string password, string location) 
            : base(name, age, email, mobile, password, location)
        {
            //
        }
    }
}