using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace candy_market.Menus
{
    class DisplayUserMenu
    {
        static bool error = false;

        // displays user menu
        public static ConsoleKeyInfo UserMenu(List<Users> candyUsers)
        {
            View userMenu = new View()
            .AddMenuText(error ? "\nPlease enter a valid user" : "Please select a user from the list below")
            .AddMenuOptions(candyUsers.Select(u => u.Name).ToList())
            .AddMenuText("Press Esc to exit.");
            Console.Write(userMenu.GetFullMenu());

            var selectedUser = Console.ReadKey();
            return selectedUser;
        }

        public static int GetValidUser(ConsoleKeyInfo selectedUser, List<Users> candyUsers)
        {
            var userInput = selectedUser.KeyChar.ToString();

            // if user clicks escape
            if (userInput == "\u001b")
            {
                return -2;
            }
            try
            {
                var userIndex = int.Parse(userInput);
                if (!(userIndex < 1) && (userIndex > candyUsers.Count))
                {
                    error = true;
                    return -1;
                }
                else
                {
                    error = false;
                    return userIndex;
                }
            }
            catch
            {
                error = true;
                return -1;
            }
        }
       
        public static Users GetUser(int userIndex, List<Users> candyUsers)
        {
            error = false;
            var user = candyUsers[userIndex - 1];
            return user;
        }
    }
}
