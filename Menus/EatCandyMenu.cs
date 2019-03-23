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
            var myCandy = db.GetCandyByUserId(userId);
            var candyNames = myCandy.Select(candy => candy.Name).ToList();

            View chooseCandyView = new View()
                .AddMenuText("Which candy do you want to eat?")
                .AddMenuOptions(candyNames)
                .AddMenuText("Press Esc to return to the Main Menu");
                Console.WriteLine(chooseCandyView.GetFullMenu());
                var candySelected = Console.ReadKey();
                var userChosenCandy = candyIntToString(candySelected, candyNames);

            sortChosenCandy(db, userChosenCandy, userId);
        }
        private static string candyIntToString(ConsoleKeyInfo candySelected, List<string> aname)
        {
            try {
                var candyPickedFromList = int.Parse(candySelected.KeyChar.ToString());
                return aname[candyPickedFromList - 1];
            }
            catch {
                return "Error";
            }
        }
        private static void sortChosenCandy(CandyStorage db, string selectedCandy, int userId)
        {
            var usersCandy = db.GetCandyByUserId(userId);
            var candyList = usersCandy.FirstOrDefault(x => x.Name == selectedCandy);

            if (candyList != null) 
            {
                var candyToUpdate = usersCandy
                .Where(x => x.Name == selectedCandy)
                .OrderBy(x => x.DateReceived)
                .First();
                candyToUpdate.isEaten = true;
                updateEatenCandy(db, candyToUpdate);

            } else {
                AddEatCandyMenu(db, userId);
            }
        }
        private static void updateEatenCandy(CandyStorage db, Candy candyToUpdate)
        {
            var candyUserAte = db.UpdateCandy(candyToUpdate);
            Console.WriteLine($"\nYou just ate a {candyUserAte.Name} that was recieved on {candyUserAte.DateReceived}, Gee wiz! That was yummy!!");
            Console.WriteLine("\nPress the Enter key to continue.");
            Console.ReadLine();
        }
    }

  }