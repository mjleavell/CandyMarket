using System;

namespace candy_market
{
    class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Users(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Users()
        {

        }
    }
}
