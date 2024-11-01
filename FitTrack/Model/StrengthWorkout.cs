using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace FitTrack.Model
{
    internal class StrengthWorkout : Workout
    {
        public int Repititions { get; set; }
        Random rand = new Random();

        public StrengthWorkout() {}
        public StrengthWorkout(string type, TimeSpan duration, DateTime date, string notes, Guid? guid, int repititions) : base(type, duration, date, notes, guid)
        {
            Repititions = repititions;
            CaloriesBurned = CalculateCaloriesBurned();
        }
        public override int CalculateCaloriesBurned()  
        {
            return Repititions * Random.Shared.Next(7, 12);
        }
        //public override int TypeInfoInput => Repititions;
        public override int TypeInfoInput
        {
            get { return Repititions; }
            set 
            {
                Repititions = value;
            }
        }


    }
}
