using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    internal class User : Person
    {

        string Country { get; set; }
        string SecurityQuestion { get; set; }
        string SecurityAnswer { get; set; }

        public User(string username, string password, string securityAnswer) : base(username, password)
        {
            SecurityAnswer = securityAnswer;
        }

        public override void SignIn()
        {
            if (SecurityQuestion != null)
            {

            }

        }
    }
}
