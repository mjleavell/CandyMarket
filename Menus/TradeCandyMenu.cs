using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace candy_market.Menus
{
    internal class TradeCandyMenu
    {
        public static void AddTradeCandyMenu(CandyStorage db, int userId, Users user, List<Users> candyUsers)
        {
            //Screen for choosing who to trade with
            var yourName = user.Name;
            var allNames = candyUsers.Select(u => u.Name).ToList();
            var everyoneButYou = allNames.Remove(yourName);
            View candyMenuTradeWho = new View()
                    .AddMenuText($"Hello {user.Name}. Who would you like to trade with?")
                    .AddMenuOptions(candyUsers.Select(u => u.Name).ToList());
            Console.Write(candyMenuTradeWho.GetFullMenu());
            var selectedTradeUser = Console.ReadKey();
            var userInput = selectedTradeUser.KeyChar.ToString();
            var ParsedTradeUser = int.Parse(userInput);
            var CandyTradeWho = DisplayUserMenu.GetUser(ParsedTradeUser, candyUsers);

            if(CandyTradeWho.Id == userId)
            {
                Console.WriteLine("\nYou want to trade with yourself?  That seems like a dumb waste of your time, but knock yourself out. . .");
                Console.ReadLine();
            }

            //Screen for choosing what candy you want
            var theirCandyBag = db.GetCandyByUserId(CandyTradeWho.Id);
            var listTheirCandyBag = theirCandyBag.Select(x => x.Name).ToList();
            var showTheirCandyBag = string.Join("\n", listTheirCandyBag);
            View candyMenuRecieveWhat = new View()
                   .AddMenuText($"This is what is in {CandyTradeWho.Name}'s candy bag:")
                   .AddMenuText(showTheirCandyBag)
                   .AddMenuText("What would you like to get in this trade?");
            Console.Write(candyMenuRecieveWhat.GetFullMenu());
            var CandyRecieveWhat = Console.ReadLine();

            //Screen for choosing which candy to give in return
            var myCandyBag = db.GetCandyByUserId(userId);
            var listMyCandyBag = myCandyBag.Select(x => x.Name).ToList();
            var showMyCandyBag = string.Join("\n", listMyCandyBag);
            View candyMenuGiveWhat = new View()
                    .AddMenuText("This is what is in your candy bag:")
                    .AddMenuText(showMyCandyBag)
                    .AddMenuText("What would you like to give in this trade?");
            Console.Write(candyMenuGiveWhat.GetFullMenu());
            var CandyGiveWhat = Console.ReadLine();

            TradeCandy(db, userId, CandyTradeWho, CandyRecieveWhat, CandyGiveWhat);
        }

        //Trade Logic
        internal static void TradeCandy(CandyStorage db, int userId, Users CandyTradeWho, string CandyRecieveWhat, string CandyGiveWhat)
        {
            var MyCandyBag = db.GetCandyByUserId(userId);
            var PieceToTrade = MyCandyBag.Find(x => x.Name.ToLower() == CandyGiveWhat.ToLower());

            var TheirCandyBag = db.GetCandyByUserId(CandyTradeWho.Id);
            var PieceToGet = TheirCandyBag.Find(x => x.Name.ToLower() == CandyRecieveWhat.ToLower());

            if(PieceToGet != null && PieceToTrade != null)
            {
                PieceToGet.UserId = userId;
                PieceToTrade.UserId = CandyTradeWho.Id;
                Console.WriteLine($"You traded your {PieceToTrade.Name} for {CandyTradeWho.Name}'s {PieceToGet.Name}!");
                Console.ReadLine();
            }
            else if(PieceToGet == null)
            {
                Console.WriteLine($"Error.  {CandyTradeWho.Name} does not have any {CandyRecieveWhat} to trade.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine($"Error. You do not have any {CandyGiveWhat} to trade.");
                Console.ReadLine();
            }
        }
    }
}