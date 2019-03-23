using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market.Menus
{
    internal class EatRandomCandyMenu
    {
        private static Random random = new Random();
        static bool error = false;

        public static void AddRandomCandyMenu(CandyStorage db, int userId)
        {
            var flavorString = GetUsersCandyFlavors(db, userId);
            if (flavorString.Count < 1)
            {
                Console.WriteLine("You have no Candy.  Press any Key to return to the Main Menu");
                Console.ReadLine();
                return;
            }

            View addRandomCandyFlavors = new View()

                .AddMenuText(error ? "Could not locate that Flavor, Try again:" : "Please type the Flavor of candy you wish to eat from the list:")
                .AddMenuOptions(flavorString)
                .AddMenuText("Press Esc to return to the Main Menu");
            Console.Write(addRandomCandyFlavors.GetFullMenu());
            var userSelection = Console.ReadKey();

            if (userSelection.Key == ConsoleKey.Escape)
                return;

            var selectedCandyFlavor = GetFlavorFromMenu(userSelection, flavorString);
            EatRandomCandy(db, selectedCandyFlavor, userId);
        }

        private static string GetFlavorFromMenu(ConsoleKeyInfo userSelection, List<string> flavors)
        {
            try
            {
                var selectedMenuInt = int.Parse(userSelection.KeyChar.ToString());
                error = false;
                return flavors[selectedMenuInt - 1];
            }
            catch (Exception)
            {
                error = true;
                return "Error";
            }
        }

        // Return the Distinct Flavor Categories for a user
        private static List<string> GetUsersCandyFlavors(CandyStorage db, int userId)
        {
            var usersCandy = db.GetCandyByUserId(userId);
            var userCandyFlavors = usersCandy
                .Select(candy => candy.FlavorCategory)
                .Distinct()
                .ToList();
            return userCandyFlavors;
            //return string.Join(", ", userCandyFlavors);
        }

        private static void EatRandomCandy(CandyStorage db, string candyFlavor, int userId)
        {
            // Get All users Candy and see if we can match one in the list with what they typed, if not show menu again?
            var usersFlavorCandy = db.GetCandyByUserId(userId);
            var flavorsList = usersFlavorCandy.FirstOrDefault(f => f.FlavorCategory == candyFlavor);

            // Found a candy flavor match so lets filter and sort by earliest date and send that to UpdateCandy Method
            if (flavorsList != null)
            {
                error = false; // Set our flag back to false
                // List<Candy> filtered by the Flavor the user typed
                var usersCandyToUpdate = usersFlavorCandy
                    .Where(candy => candy.FlavorCategory == candyFlavor)
                    .ToList();

                // Now let us get a Random piece of candy from the list of candies filtered by Flavor
                var userRandomCandy = usersCandyToUpdate[random.Next(usersCandyToUpdate.Count)];

                // Get the count of that flavor candy by name.  Need to filter by oldest of more than one
                var randomCandyCount = usersCandyToUpdate.Where(candy => candy.Name == userRandomCandy.Name).Count();

                if (randomCandyCount > 1)
                {
                    var randomCandyToUpdate = usersFlavorCandy
                        .Where(candy => candy.Name == userRandomCandy.Name)
                        .OrderBy(candy => candy.DateReceived)
                        .FirstOrDefault();
                    // Update isEaten to true and pass the updated Candy on to the CandyStorage
                    randomCandyToUpdate.isEaten = true;
                    UpdateRandomCandy(db, randomCandyToUpdate);
                }
                else
                {
                    var randomCandyToUpdate = usersFlavorCandy
                        .Where(candy => candy.Name == userRandomCandy.Name)
                        .FirstOrDefault();
                    // Update isEaten to true and pass the updated Candy on to the CandyStorage
                    randomCandyToUpdate.isEaten = true;
                    UpdateRandomCandy(db, randomCandyToUpdate);
                }
            }
            else
            {
                // Must have typed a flavor wrong or couldn't find what you typed
                error = true;
                AddRandomCandyMenu(db, userId);
            }
        }

        private static void UpdateRandomCandy(CandyStorage db, Candy candyToUpdate)
        {
            var UpdatedCandy = db.UpdateCandy(candyToUpdate);
            Console.WriteLine($"\nYou just ate a {UpdatedCandy.Name} that was recieved on {UpdatedCandy.DateReceived}");
            Console.WriteLine("\nPress the Enter key to continue.");
            Console.ReadLine();
        }
    }
}