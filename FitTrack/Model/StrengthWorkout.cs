using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    internal class StrengthWorkout : Workout
    {
        int Repititions { get; set; }
        

        public StrengthWorkout(string type, int caloriesBurned, TimeSpan duration, DateTime date, string notes, Guid? guid, int repititions) : base(type, caloriesBurned, duration, date, notes, guid)
        {
            Repititions = repititions;
        }
        public override int CalculateCaloriesBurned()
        {
            return 0;
        }
    }
}
