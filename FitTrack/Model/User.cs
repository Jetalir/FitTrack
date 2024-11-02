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
        public string Country { get; set; }
        public bool SignedIn = false;
        public string SecurityQuestion { get; set; }
        string SecurityAnswer { get; set; }
        public Guid UserId;

        public User()
        {

        }
        public User(string username, string password) : base(username, password)
        {
            
        }
        public User(string username, string password,string securityQuestion, string securityAnswer, string country, bool signedIn) : base(username, password)
        {
            SecurityAnswer = securityAnswer;
            Country = country;
            SignedIn = signedIn;
            SecurityQuestion = securityQuestion;
            UserId = Guid.NewGuid();
        }
        public override void SignIn() //Flag the user as Signed in
        {
            SignedIn = true;
        }

        public void ResetPassword(string securityAnswer)
        {
            
        }
        public void SignOut()
        {
            SignedIn = false;
        }
        public string GetSecurityAnswer()
        {
            return SecurityAnswer;
        }

        public Guid GetUserId()
        {
            return UserId;
        }
    }
}
