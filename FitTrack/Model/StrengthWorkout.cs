using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Model
{
    internal class StrengthWorkout : Workout
    {
        public StrengthWorkout()
        {
            
        }
        public StrengthWorkout(string Type, int CaloriesBurned, TimeSpan Duration, DateTime Date, string Notes) : base(Type, CaloriesBurned, Duration, Date, Notes)
        {

        }
        public override int CalculateCaloriesBurned()
        {
            return 0;
        }
    }
}
