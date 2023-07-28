using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adventure
{
    public class Player
    {
        public string Name { get; }                         // The name of the player 
        public List<Keys> Keys { get; set; }                   // The keys that the player has 
        public List<Weapons> Weapons { get; }               // The weapons that the player has 
        public List<string> Items { get; }                  // List to store the items the player has picked up

        public List<string> CollectedItems { get; }
        public List<Weapons> CollectedWeapons { get; }
        public int Power { get; private set; }              // The power level of the player 
        public int Lives { get; private set; }              // The number of lives or tries left for the player 
        public List<Powers> Powers { get; private set; }
        public Room CurrentRoom { get; set; }

        public Player(string name, Room currentRoom)
        {
            Name = name;
            CurrentRoom = currentRoom;
            Keys = new List<Keys>();
            Items = new List<string>();                             // Initialize the Items list
            Weapons = new List<Weapons>();
            CollectedItems = new List<string>();
            CollectedWeapons = new List<Weapons>();
            Power = 200;
            Lives = 3;
            Powers = new List<Powers>();
        }
        public bool HasKey(string keyName)
        {
            return Keys.Any(key => key.Name == keyName);
        }
        public void RemoveKey(string keyName)
        {
            Keys.RemoveAll(key => key.Name == keyName);
        }
        public void Move(string doorName)
        {
            // Check if the player is trying to exit the game
            if (doorName == "0")
            {
                Console.WriteLine("Thank you for Playing! Have a nice Day.");
                Environment.Exit(0);
            }

            // Find the selected door in the current room
            Door selectedDoor = CurrentRoom.Doors.FirstOrDefault(door => door.Name == "Door Nr." + doorName);

            if (selectedDoor != null)
            {
                // If the selected door requires a key, check if the player has the key
                if (selectedDoor.RequiresKey)
                {
                    Console.WriteLine($"You need {selectedDoor.KeyRequired} to open this door.");
                    if (HasKey(selectedDoor.KeyRequired))
                    {
                        // If the player has the key, unlock the door and move to the destination room
                        RemoveKey(selectedDoor.KeyRequired);
                        CurrentRoom = selectedDoor.Destination;
                        Console.WriteLine($"You have unlocked the door and entered the {CurrentRoom.Name}.");
                    }
                    else
                    {
                        Console.WriteLine("You don't have the required key to open this door.");
                    }
                }
                else
                {
                    // If the door does not require a key, move to the destination room
                    CurrentRoom = selectedDoor.Destination;
                    Console.WriteLine($"You have entered the {CurrentRoom.Name}.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }



        //public void PickUpItem(string item)
        //{
        //    _player.CurrentRoom.PickUpItem(item);
        //}

        public void PickUpItem(string itemName)
        {

            // Check if the item is available in the current room
            if (CurrentRoom.HasItem(itemName))
            {
                // Add the item to the player's inventory
                Items.Add(itemName);
                // Remove the item from the room
                CurrentRoom.Items.Remove(itemName);
                Console.WriteLine($"You picked up {itemName}.");
            }
            else
            {
                Console.WriteLine($"The item '{itemName}' is not available in this room.");
            }


            if (itemName.StartsWith("key"))
            {
                if (this.CurrentRoom.Keys.Count == 0)
                {
                    Console.WriteLine("There are NO Keys in here.");
                    return;
                }

                Keys pickedKey = this.CurrentRoom.Keys.First();
                this.Keys.Add(pickedKey);
                this.CurrentRoom.Keys.RemoveAt(0);
                Console.WriteLine($"You found The {pickedKey.Name}.");
            }
            else if (itemName.StartsWith("weapon"))
            {
                if (this.CurrentRoom.Weapons.Count == 0)
                {
                    Console.WriteLine("There are NO Weapons in this Room.");
                    return;
                }

                foreach (var weaponInRoom in this.CurrentRoom.Weapons)
                {
                    Console.WriteLine(weaponInRoom.Name);
                }
                Console.WriteLine("select one of the above listed Weapons.");
                string choosenWeapon = Console.ReadLine();
                Console.WriteLine($"You choose {choosenWeapon}.");

                var weapon = this.CurrentRoom.Weapons.FirstOrDefault(x => x.Name == choosenWeapon);
                if (weapon == null)
                {
                    Console.WriteLine("There are NO Weapons in here.");
                }
                this.Weapons.Add(weapon);
                this.CurrentRoom.Weapons.Remove(weapon);
            }
            else if (itemName.StartsWith("power"))
            {
                //int powerValue = int.Parse(item.Substring(5)); // Get the power value after "power"
                //Power += powerValue;
                //Console.WriteLine($"You picked up a power item and your power is now {Power}.");
                if (this.CurrentRoom.Powers.Count == 0)
                {
                    Console.WriteLine("There are NO Powers in this Room.");
                    return;
                }

                foreach (var powerInRoom in this.CurrentRoom.Powers)
                {
                    Console.WriteLine(powerInRoom.Name);
                }
                Console.WriteLine("select one of the above listed Powers.");
                string choosenPower = Console.ReadLine();
                Console.WriteLine($"You choose {choosenPower}.");

                var power = this.CurrentRoom.Powers.FirstOrDefault(x => x.Name == choosenPower);
                if (power == null)
                {
                    Console.WriteLine("There are NO Powers in here.");
                }
                this.Powers.Add(power);
                this.CurrentRoom.Powers.Remove(power);
            }
            else
            {
                // Invalid item
                Console.WriteLine($"invalid input");
            }
        }



        //public void PickUpAvailableItems()
        //{
        //    CurrentRoom.PickUpItem(this);
        //}
        public void PickUpAvailableItems()
        {
            if (CurrentRoom.RequiresKey && !CurrentRoom.KeyHasBeenUsed)
            {
                // Check if the player has the required key to open the door
                var requiredKey = CurrentRoom.KeyRequired;
                if (Keys.Any(key => key.Name == requiredKey))
                {
                    // Use the key to open the door
                    CurrentRoom.RemoveKey(requiredKey);
                    CurrentRoom.KeyHasBeenUsed = true;
                    Console.WriteLine($"You used {requiredKey} to open the door.");
                }
                else
                {
                    Console.WriteLine($"You need {requiredKey} to open this door.");
                    return;
                }
            }

            CurrentRoom.PickUpItem(this);
        }


        public void UseItem()
        {
            // Prompt the user to choose an item to use
            Console.WriteLine("Choose an item to use:");
            Console.WriteLine("1. Use collected item");
            Console.WriteLine("2. Use collected weapon");
            Console.Write("Enter the option number: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                // Use collected item
                if (CollectedItems.Count > 0)
                {
                    Console.WriteLine("Choose an item to use:");
                    for (int i = 0; i < CollectedItems.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {CollectedItems[i]}");
                    }
                    Console.Write("Enter the item number: ");
                    if (int.TryParse(Console.ReadLine(), out int itemIndex))
                    {
                        if (itemIndex > 0 && itemIndex <= CollectedItems.Count)
                        {
                            string item = CollectedItems[itemIndex - 1];
                            // Implement the logic to use the selected item
                            Console.WriteLine($"You used {item}.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid item number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }
                else
                {
                    Console.WriteLine("You don't have any collected items to use.");
                }
            }
            else if (option == "2")
            {
                // Use collected weapon
                if (CollectedWeapons.Count > 0)
                {
                    Console.WriteLine("Choose a weapon to use:");
                    for (int i = 0; i < CollectedWeapons.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {CollectedWeapons[i].Name}");
                    }
                    Console.Write("Enter the weapon number: ");
                    if (int.TryParse(Console.ReadLine(), out int weaponIndex))
                    {
                        if (weaponIndex > 0 && weaponIndex <= CollectedWeapons.Count)
                        {
                            Weapons weapon = CollectedWeapons[weaponIndex - 1];
                            // Implement the logic to use the selected weapon
                            Console.WriteLine($"You used {weapon.Name}.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid weapon number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number.");
                    }
                }
                else
                {
                    Console.WriteLine("You don't have any collected weapons to use.");
                }
            }
            else
            {
                Console.WriteLine("Invalid option. Please enter 1 or 2.");
            }
        }
        public void Attack(string monsterName)
        {
            // Try to attack a monster in the room
            Monsters monster = null;
            switch (monsterName)
            {
                case "BabyMonster":
                    monster = new BabyMonsters();
                    break;
                case "BigMonster":
                    monster = new BigMonster();
                    break;
                default:
                    Console.WriteLine($"There is no {monsterName} in this room.");
                    return;
            }
            if (CurrentRoom.HasMonster(monster))
            {
                // Check if the player has any weapon 
                if (Weapons.Count > 0)
                {
                    // Choose a random weapon from the player's inventory 
                    Random random = new Random();
                    int index = random.Next(Weapons.Count);

                    Weapons weapon = Weapons[index];
                    // Calculate the damage done by the player and the monster
                    int playerDamage = weapon.Strength - monster.Defense;
                    int monsterDamage = monster.Strength - weapon.Defense;

                    // Update the power levels of the player and the monster 
                    Power -= monsterDamage;
                    monster.Power -= playerDamage;

                    // Display the result of the attack 
                    Console.WriteLine($"You attacked {monsterName} with {weapon.Name} and did {playerDamage} damage.");
                    Console.WriteLine($"{monsterName} attacked you back and did {monsterDamage} damage.");
                    Console.WriteLine($"Your power level is now {Power}.");
                    Console.WriteLine($"{monsterName}'s power level is now {monster.Power}.");

                    // Check if the player or the monster is defeated 
                    if (Power == 0)
                    {
                        // The player is defeated 
                        Console.WriteLine("You have been defeated!");
                        Lives--;
                        Console.WriteLine($"You have {Lives} lives left.");
                        if (Lives > 0)
                        {
                            // The player can try again from the beginning 
                            Console.WriteLine("You can start over from the same room.");

                        }
                        else
                        {
                            // The player has no more lives left 
                            Console.WriteLine("Game over!");
                            Environment.Exit(0);
                        }
                    }
                }
            }
        }
    }
}

