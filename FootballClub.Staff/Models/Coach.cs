using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballClub.Staff.Controllers
{
    internal class Coach : Employee
    {
        public Coach(string firstName, string lastName, string email, int id, DateTime startedWorkingDate, Address address,
            double salary, int gamesWon = 0, int tiedGames = 0, int lostGames = 0)
            : base(firstName, lastName, email, id, startedWorkingDate, address, salary)
        {
            GamesWon = gamesWon;
            TiedGames = tiedGames;
            LostGames = lostGames;
        }
        public int GamesWon { get; set; }

        public int TiedGames { get; set; }

        public int LostGames { get; set; }

        public void WinGame()
        {
            GamesWon++;
        }

        public void TieGame()
        {
            TiedGames++;
        }

        public void LoseGame()
        {
            LostGames++;
        }

        public string GetStatistics()
        {
            int totalGames = GamesWon + TiedGames + LostGames;
            return $"Won games: {GamesWon}\nTied games: {TiedGames}\nLostGames: {LostGames}\nWin percentage: {Math.Round(((double)GamesWon/(double)totalGames) * 100, 2)}%";
        }
    }
}
