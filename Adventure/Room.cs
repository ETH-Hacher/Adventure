using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Adventure
{
    public class Room
    {
        public string Name { get; set; }
        public Func<bool> AdditionalInfo { get; set; }
        public string Description { get; }
        //{
        //    get
        //    {
        //        return $"That's the Room Nr. {ID} and it's called The {Name}, here  {AdditionalInfo}";
        //    }
        //}

        public int ID { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<Keys> Keys { get; }                                   // A list of keys in the room
        public List<Weapons> Weapons { get; }                               // A list of weapons in the room
        public Monsters Monster { get; set; }                          // A list of monsters in the room
        public List<Door> Doors { get; }
        public List<string> Items { get; }
        public List<Powers> Powers { get; }
        public bool RequiresKey { get; internal set; }
        public string KeyRequired { get; internal set; }
        public bool KeyHasBeenUsed { get; internal set; }


        // Key key.nDoor.1.sDoor.2

        private readonly Dictionary<string, Door> _keyDoorMap = new Dictionary<string, Door>();


        public bool HasKey(string keyName)
        {
            return Keys.Any(key => key.Name == keyName);
        }

        public void RemoveKey(string keyName)
        {
            Keys.RemoveAll(key => key.Name == keyName);
        }

        public Room(int id, string name, string description, List<Door> doors, List<string> items, int x, int y, List<Powers> powers = null , List<Weapons> weapons = null)
        {
            ID = id;
            X = x;
            Y = y;
            Name = name;
            Items = items;
            Doors = doors;
            if (powers == null)
            {
                Powers = new List<Powers>();
            }
            else
            {
                Powers = powers;
            }
            if (weapons == null)
            {
                Weapons = new List<Weapons>();
            }
            else
            {
                Weapons = weapons;
            }
            Description = description;
            AdditionalInfo = null;
            Keys = new List<Keys>();
           


            // Convert key names to Keys objects and add them to the Keys list
            
            //foreach (var keyName in items.Where(item => item.StartsWith("key")))
            //{
            //    var key = new Keys(keyName.Substring(4));
            //    Keys.Add(key);
            //}



            // Initialize the key/door dictionary
            _keyDoorMap = new Dictionary<string, Door>();
            foreach (var door in doors)
            {
                if (door.Name != null)
                {
                    _keyDoorMap[door.Name] = door;
                }
            }
        }

        public bool HasItem(string item)
        {
            return Items.Contains(item);
        }
        public bool HasMonster(Monsters monster)
        {
            // Check if the room has any monster
            return Monster != null;
        }
       
        public bool HasWeapon(Weapons weapon)
        {
            // Check if the room has a specific weapon
            return Weapons.Contains(weapon);
        }
        public void RemoveWeapon(Weapons weapon)
        {
            // Remove a weapon from the room
            Weapons.Remove(weapon);
        }
        
        public bool HasDoor(string door)
        {
            // Check if the room has a specific door
            return Doors.Any(d => d.Name == door);
        }
        public Room GetDestination(string door)
        {
            // Get the destination of a door
            return Doors.FirstOrDefault(d => d.Name == door)?.Destination;
        }

        public void PickUpItem(Player player)
        {
            if (Items.Count > 0 || Weapons.Count > 0 || Keys.Count > 0 || Powers.Count > 0)
            {
                Console.WriteLine("Available items in the current room:");
                Console.WriteLine("Items: ");
                foreach (var item in Items)
                {
                    Console.WriteLine($"- {item}");
                }

                Console.WriteLine("Weapons: ");
                foreach (var weapon in Weapons)
                {
                    Console.WriteLine($"- {weapon.Name}");
                }

                Console.WriteLine("Keys: ");
                foreach (var key in Keys)
                {
                    Console.WriteLine($"- {key.Name}");
                }

                Console.WriteLine("Powers: ");
                foreach (var power in Powers)
                {
                    Console.WriteLine($"- {power.Name} (Power: {power.Value})");
                }

                Console.WriteLine("Enter the name of the item you want to pick up:");
                string itemName = Console.ReadLine();
                player.PickUpItem(itemName);                         // Call the PickUpItem method of the Player class

                if (Items.Any(item => item == itemName))
                {
                    player.Items.Add(itemName);
                    Items.Remove(itemName);
                    Console.WriteLine($"You picked up {itemName}.");
                }
                else if (Weapons.Any(weapon => weapon.Name == itemName))
                {
                    Weapons weapon = Weapons.FirstOrDefault(w => w.Name == itemName);
                    player.Weapons.Add(weapon);
                    Weapons.Remove(weapon);
                    Console.WriteLine($"You picked up {itemName}.");
                }
                else if (Keys.Any(key => key.Name == itemName))
                {
                    Keys key = Keys.FirstOrDefault(k => k.Name == itemName);
                    player.Keys.Add(key);
                    Keys.Remove(key);
                    Console.WriteLine($"You picked up {itemName}.");
                }
                else if (Powers.Any(power => power.Name == itemName))
                {
                    Powers power = Powers.FirstOrDefault(p => p.Name == itemName);
                    player.Powers.Add(power);
                    Powers.Remove(power);
                    Console.WriteLine($"You picked up {itemName}.");
                }
                else
                {
                    Console.WriteLine("Item not found in the room.");
                }
            }
            else
            {
                Console.WriteLine("Nothing to pick up in this room.");
            }
        }



        public bool TryOpenDoorWithKey(string keyName)
        {
            // Check if the player has the matching key
            var matchingKey = Keys.FirstOrDefault(key => key.Name == keyName);
            if (matchingKey != null)
            {
                // If the player has the matching key, remove it from their inventory
                Keys.Remove(matchingKey);
                return true;
            }
            return false;
        }


        //public bool TryOpenDoorWithKey(string keyName)
        //{
        //    if (_keyDoorMap.TryGetValue(keyName, out Door door))
        //    {
        //        return door.Open;
        //    }
        //    return false;
        //}

        //public bool TryOpenDoorWithKey(string keyName)
        //{
        //    // Check if the player has the matching key
        //    if (Keys.Any(key => key.Name == keyName))
        //    {
        //        // If the player has the matching key, remove it from their inventory
        //        Keys.RemoveAll(key => key.Name == keyName);
        //        return true;
        //    }
        //    return false;
        //}


    }
}
