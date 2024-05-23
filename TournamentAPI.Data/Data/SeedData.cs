using Bogus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TournamentAPI.Core.Entities;

/*
 skapa seed data
1. installera bogus
2. skapa metoden InitAsync -> detta skall kontrollera om det finns turneringar eller games i databasen först. om det 
   finns då avslutar du metoden
3. Skapa en Faker instans: Faker används för att generera falska data. Här används "sv" -> dvs svenska språket som parameter
4. Generera data genom metoder 
    - generateTornaments
    - generateGames
5. spara data databas till databasen 
 
 */

namespace TournamentAPI.Data.Data
{
    public class SeedData
    {
        private static Faker faker;

        public static async Task InitAsync(TournamentAPIContext tournamentAPIContext)
        {
            //cancel the seeddata initalization if we have data in tournament database table
            if(await tournamentAPIContext.Tournaments.AnyAsync())
            {
                return;
            }

            faker = new Faker("sv");

            //generera turneringar
            var tournaments = GenerateTournaments(10);
            await tournamentAPIContext.AddRangeAsync(tournaments);

            //generera spel
            var games = GenerateGames(tournaments);
            await tournamentAPIContext.AddRangeAsync(games);

            //spara ändringar till databasen
            await tournamentAPIContext.SaveChangesAsync();

        }

        private static IEnumerable<Game> GenerateGames(IEnumerable<Tournament> tournaments)
        {
            var games = new List<Game>();

            foreach(var tournament in tournaments)
            {
                int numberOfGames = faker.Random.Int(4, 8);

                for(int i = 0; i < numberOfGames; i++)
                {
                    var country1 = faker.Address.Country().Substring(0,3).ToUpper();
                    var country2 = faker.Address.Country().Substring(0, 3).ToUpper();

                    while (country1 == country2)
                    {
                        country2 = faker.Address.Country();
                    }

                    var title = $"{country1}-{country2}";
                    var time = faker.Date.Soon();

                    var game = new Game
                    {
                        Title = title,
                        Time = time,
                        Tournament = tournament,
                        TournamentId = tournament.Id,
                    };

                    tournament.Games.Add(game);
                    games.Add(game);
                }
            }
            return games;
        }

        private static IEnumerable<Tournament> GenerateTournaments(int numOfTournaments)
        {
            var tournaments = new List<Tournament>();

            for (int i = 0; i < numOfTournaments; i++)
            {
                var personName = faker.Name.FirstName();
                var title = $"{personName}'s Tournament";
                var startDate = faker.Date.Future();

                var tournament = new Tournament()
                {
                    Title = title,
                    StartDate = startDate,
                    Games = new List<Game>()
                };
                tournaments.Add(tournament);
            }
            return tournaments;
        }
    }
}
