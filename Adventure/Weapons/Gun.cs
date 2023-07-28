using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public class Gun : Weapons
    {
        public Gun()
        {
            Name = "Gun";
            Strength = 100;
            Defense = 25;
        }
    }
}
