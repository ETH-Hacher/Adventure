using System.Net.NetworkInformation;

namespace Adventure
{
    public class Door
    {
        public string Name { get; }                             // The name of the door
        public Room Destination { get; }                       // The destination of the door
        public Room ConnectedRoom { get; }
        public string KeyRequired { get; set; }                 // the name of the key required to open the door
        public DoorDirection Direction { get; set; }
        public bool RequiresKey { get; set; }
        public bool KeyHasBeenUsed { get; set; }
        public bool Open { get; set; }                          // Add the Open property to Door class



        
        public Door(string name, DoorDirection direction,bool requiresKey ) 
        {
            Name = name;
            Direction = direction;
            RequiresKey = requiresKey;
        }

        public bool IsOpen
        {
            get
            {
                return (RequiresKey && KeyHasBeenUsed) || !RequiresKey;
            }
        }

    }

    public enum DoorDirection
    {
        Up, Down, Left, Right
    }
}