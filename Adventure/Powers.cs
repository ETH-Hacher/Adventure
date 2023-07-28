using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    public class Powers
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public Powers(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }

}
