using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClub.Staff.Controllers
{
    internal class Player : Employee
    {

        public Player()
        {
            
        }
        public Player(string firstName, string lastName, string email, int id, DateTime startedWorkingDate, Address address,
            double salary, int goalsScored = 0, int assistsMade = 0)
            : base(firstName, lastName, email, id, startedWorkingDate, address, salary){
            GoalsScored = goalsScored;
            AssistsMade = assistsMade;
        }
        public int GoalsScored { get; set; }

        public int AssistsMade { get; set; }

        public void AddGoalBonus(int goalsScored,double bonus = 100)
        {
            if(goalsScored > 0)
            {
                base.IncreaseSalary(goalsScored * bonus);
                GoalsScored += goalsScored;
            }
        }

        public void AddAssistBonus(int assistsMade, double bonus = 50)
        {
            if(assistsMade > 0)
            {
                base.IncreaseSalary(assistsMade * bonus);
                this.AssistsMade += assistsMade;
            }
           
        }

        public override int GetWorkExperience()
        {
            //first year doesn't count because the player has to go through training camp
            return base.GetWorkExperience() - 1;
        }
    }
}
