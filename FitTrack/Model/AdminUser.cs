using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    internal class AdminUser : User
    {

        public AdminUser(string username, string password, string securityQuestion, string securityAnswer, string country, bool signedIn) : base(username, password, securityQuestion, securityAnswer, country, signedIn)
        {

        }

        public void ManageAllWorkouts()
        {

        }
    }
}
