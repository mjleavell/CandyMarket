using System;
using System.Collections.Generic;
using System.Text;

namespace candy_market.SeedData
{
    static class DefaultCandy
    {
        public static void SeedCandy()
        {
            var candyDb = new CandyStorage();

            var dummyCandies = new List<Candy>
            {
                new Candy("Reeses", "Chocolate", DateTime.Now, "Mars",1),
                new Candy("Reeses", "Chocolate", DateTime.Now, "Mars",1),
                new Candy("Skittles", "Sour", DateTime.Now, "Mars",2),
                new Candy("Skittles", "Sour", DateTime.Now, "Mars",2),
                new Candy("Snickers", "Chocolate", DateTime.Now, "Hershey",3),
                new Candy("Snickers", "Chocolate", DateTime.Now, "Hershey",3),
                new Candy("Snickers", "Chocolate", DateTime.Now, "Mars",4),
                new Candy("Baby Ruth", "Chocolate", DateTime.Now, "Mars",1),
                new Candy("Kit Kat", "Chocolate", DateTime.Now, "Mars",2),
                new Candy("Air Heads", "Sour", DateTime.Now, "Haribo",3),
                new Candy("Almond Joy", "Chocolate", DateTime.Now, "Nestle",4),
                new Candy("Almond Joy", "Chocolate", DateTime.Now, "Nestle",4),
                new Candy("Starburst", "Fruity", DateTime.Now, "Nestle",1),
                new Candy("M&M", "Chocolate", DateTime.Now, "Mars",2),
                new Candy("Gummy Worms", "Fruit Gummy", DateTime.Now, "Trolli",3),
                new Candy("Hershey Kisses", "Chocolate", DateTime.Now, "Hershey",4),
                new Candy("Kinder Egg", "Chocolate", DateTime.Now, "Nestle",1),
                new Candy("Flake", "Chocolate", DateTime.Now, "Cadbury",2),
                new Candy("Candy Corn", "Other", DateTime.Now, "Mars",3),
                new Candy("Licorice", "Salty", DateTime.Now, "Mars",4),
            };
            foreach (var candy in dummyCandies)
            {
                candyDb.SaveNewCandy(candy);
            }
        }
    }
}
