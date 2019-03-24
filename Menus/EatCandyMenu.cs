using System;
using System.Collections.Generic;
using System.Linq;

namespace candy_market.Menus
{
  internal class EatCandyMenu
  {
    public static void AddEatCandyMenu(CandyStorage db, int userId)
    {
           View EatCandyMenuView = new View()
            .AddMenuOption("Just so that we're clear, you want to eat some candy from your collection, right?")
            .AddMenuText("Press Esc to exit.");
            Console.Write(EatCandyMenuView.GetFullMenu());
            var userSelection = Console.ReadKey().KeyChar.ToString();
            var userSelectionToNumber = int.Parse(userSelection);
            if (userSelectionToNumber == 1) {
                ListingUserCandy(db, userId);
            } else {
                Console.WriteLine("Error");
            }
       }
        internal static void ListingUserCandy(CandyStorage db, int userId)
        {
            // Getting all of the user's candy by their ID
            var myCandy = db.GetCandyByUserId(userId);
            // Getting only the candy's name from that list
            var candyNames = myCandy.Select(candy => candy.Name).ToList();
            // Showing the user all of their candy options eat 
            View chooseCandyView = new View()
                .AddMenuText("Which candy do you want to eat?")
                .AddMenuOptions(candyNames)
                .AddMenuText("Press Esc to return to the Main Menu");
                Console.WriteLine(chooseCandyView.GetFullMenu());
                // Getting the number from the user's choice
                var candySelected = Console.ReadKey();
                // Calling a method that converts the int to a string
                var userChosenCandy = candyIntToString(candySelected, candyNames);

            sortChosenCandy(db, userChosenCandy, userId);
        }
        private static string candyIntToString(ConsoleKeyInfo candySelected, List<string> candies)
        {
            try {
                var candyPickedFromList = int.Parse(candySelected.KeyChar.ToString());
                return candies[candyPickedFromList - 1];
            }
            catch {
                return "Error";
            }
        }
        private static void sortChosenCandy(CandyStorage db, string selectedCandy, int userId)
        {
            // Getting all of the user's candy by their ID
            var usersCandy = db.GetCandyByUserId(userId);
            // Getting the first candy that matches what the user picked and what's in the list
            var candyList = usersCandy.FirstOrDefault(x => x.Name == selectedCandy);
            // If there's a matching candy in the list
            if (candyList != null) 
            {
                var candyToUpdate = usersCandy
                .Where(x => x.Name == selectedCandy)
                // making sure that we eat the candy that we received first so that we don't waste candy
                .OrderBy(x => x.DateReceived)
                .First();
                // Since our GetCandyByUserId method only returns "isEaten = false" candy
                candyToUpdate.isEaten = true;
                updateEatenCandy(db, candyToUpdate);

            } else {
                AddEatCandyMenu(db, userId);
            }
        }
        private static void updateEatenCandy(CandyStorage db, Candy candyToUpdate)
        {
            var candyUserAte = db.UpdateCandy(candyToUpdate);
            Console.WriteLine($"\nYummy! that {candyUserAte.Name} was absolutely delicious!");
            Console.WriteLine("\nPress the Enter key to continue.");
            Console.ReadLine();
        }
    }

  }