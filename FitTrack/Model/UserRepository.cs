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
        static List<string> SecurityQuestion = new List<string>() 
        {
            "Namnet på ditt första husdjur",
            "Ditt favorit nummer"
        };

        static List<User> Users = new List<User>() // List of all active users
        {
            new AdminUser("Jake", "Jake1234", SecurityQuestion[0], "Bob", "United States", false),
            new User("Jet", "Jet1234",  SecurityQuestion[1], "8", "Kosovo", false)
        };
        
        public void AddUser(string name, string pass, string securityquestion, string securityanswer, string country) // Registeres a new user
        {
            Users.Add(new User(name, pass, securityquestion, securityanswer, country, false ));
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
        public User? AssignSignedIn() // Returns Signed in user
        {
            foreach (var user in Users)
            {
                if (user.SignedIn)
                {
                    return user;
                }
            }
            return null;
        }

        public List<string> GetSecurityQuestions()
        {
            return SecurityQuestion;
        }
    }
}
