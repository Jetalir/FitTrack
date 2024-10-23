using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FitTrack.Model
{
    internal class User : Person
    {

        string Country { get; set; }
        string SecurityQuestion = "Your first pet name";
        string SecurityAnswer { get; set; }
        bool SignedIn = false;

        public User()
        {

        }
        public User(string username, string password) : base(username, password)
        {
            
        }
        public User(string username, string password, string securityAnswer, string country, bool signedIn) : base(username, password)
        {
            SecurityAnswer = securityAnswer;
            Country = country;
            SignedIn = signedIn;
        }
        public override void SignIn() //Flag the user as Signed in
        {
            SignedIn = true;
        }

        public void ResetPassword(string securityAnswer)
        {

        }
    }
}
