using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public class Keys
    {
        public string Name { get; set; }
        public string DoorName { get; set; }

        public Keys(string name,string doorName)
        {
            Name = name;
            DoorName = doorName;
        }
    }
}
