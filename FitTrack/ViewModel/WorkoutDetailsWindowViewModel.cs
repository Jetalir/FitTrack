using FitTrack.Model;
using FitTrack.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.ViewModel
{
    internal class WorkoutDetailsWindowViewModel : ViewModelBase
    {
        WorkoutRepository workoutRepository = new WorkoutRepository();

		private Workout workout;
		public Workout Workout
		{
			get { return workout; }
			set 
			{ 
				workout = value; 

			}
		}

	}
}
