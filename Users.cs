using System;
using System.Collections.Generic;
using System.Text;

namespace candy_market
{
    class Users
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Users(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
