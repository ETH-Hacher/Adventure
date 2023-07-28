using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adventure
{
    public class BigMonster : Monsters
    {
        public BigMonster() 
        {
            Name = "BigMonster";
            Strength = 200;
            Defense = 50;
            Power = 200;
        }
    }
}
