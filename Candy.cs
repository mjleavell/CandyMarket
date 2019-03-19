using System;

namespace candy_market
{
    internal class Candy
    {
        public string Name { get; set; }
        public FlavorCategory FlavorCategory { get; set; }
        public DateTime DateReceived { get; set; }
        public bool isEaten { get; set; }
        public string Manufacturer { get; set; }

    }
}