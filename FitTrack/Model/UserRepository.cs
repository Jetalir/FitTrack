using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace FitTrack.Model
{
    internal class UserRepository //Class to manage Users
    {
       
        List<User> Users = new List<User>() // List of all active users
        {
            new User("Jet", "Jet1234", "Hey", "Kosovo", false),
            new AdminUser("Jake", "Jake1234", "Heyo", "USA", false)
        };
        
        public void AddUser(string name, string pass, string securityquestion, string country) // Registeres a new user
        {
            Users.Add(new User(name, pass, securityquestion, country, false));
        }

        public User? GetUserByName(string name) // Returns the user with same name
        {
            foreach (var user in Users)
            {
                if (user.Username == name)
                {
                    return user;
                }
            }
            return null;
        }
    }
}
