using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market.Menus
{
    internal class EatRandomCandyMenu
    {
        static bool error = false;

        public static void AddRandomCandyMenu(CandyStorage db, int userId)
        {
            var flavorString = GetUsersCandyFlavors(db, userId);

            View addRandomCandyFlavors = new View()
                 
                .AddMenuText(error ? "Could not locate that Flavor, Try again:" : "Please type the Flavor of candy you wish to eat from the list:")
                .AddMenuText($"[ {flavorString} ]");
            Console.Write(addRandomCandyFlavors.GetFullMenu());
            var selectedCandyFlavor = Console.ReadLine();

            EatRandomCandy(db, selectedCandyFlavor, userId);
        }

        // Return the Distinct Flavor Categories for a user
        private static string GetUsersCandyFlavors(CandyStorage db, int userId)
        {
            var usersCandy = db.GetCandyByUserId(userId);
            var userCandyFlavors = usersCandy
                .Select(candy => candy.FlavorCategory)
                .Distinct()
                .ToList();
            return string.Join(", ", userCandyFlavors);
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
                var usersCandyToUpdate = usersFlavorCandy
                    .Where(candy => candy.FlavorCategory == candyFlavor)
                    .OrderBy(candy => candy.DateReceived)
                    .First();

                // We got our candy so lets set the isEaten to 'True'
                usersCandyToUpdate.isEaten = true;

                db.UpdateCandy(usersCandyToUpdate);
            }
            else
            {
                // Must have typed a flavor wrong or couldn't find what you typed
                error = true;
                AddRandomCandyMenu(db, userId);
            }
        }
    }
}