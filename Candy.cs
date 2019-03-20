using System;

namespace candy_market
{
    internal class Candy
    {
        public string Name { get; set; }
        public string FlavorCategory { get; set; }
        public DateTime DateReceived { get; set; }
        public bool isEaten { get; set; }
        public string Manufacturer { get; set; }
        public int UserId { get; set; }

        public Candy(string name, string flavor, DateTime createdTime, string manufacturer, int userId)
        {
            Name = name;
            FlavorCategory = flavor;
            DateReceived = createdTime;
            isEaten = false;
            Manufacturer = manufacturer;
            UserId = userId;
        }
    }
}