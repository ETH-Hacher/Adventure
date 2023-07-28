using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventure
{
    internal class Program
    {
        static Player _player;
        static Map _map;


        static void Main(string[] args)
        {
            _map = new Map();
            _player = new Player("Player", _map.Rooms[0]);
            _player.CurrentRoom = _map.Rooms[0];

            //var key1 = new Keys();
            //Dictionary<Keys, Door> _doorMap = new Dictionary<Keys, Door>();
            //_doorMap.Add(key1, new Door(DoorDirection.Up, true));

            //Door correspondingDoor = _doorMap[key1];

            //var monster = new Monsters();

            string decor = "########################################";
            Console.WriteLine($"{decor}{decor}{decor} \n" + decor + "      WELCOME TO THE ADVETURE GAME      " + decor + $"\n{decor}{decor}{decor}");


            //Console.WriteLine(_player.CurrentRoom.Description);

            //Console.WriteLine($"You are in the {_player.CurrentRoom.Name}");
            while (true)
            {
                
                string roomsMenu = $"\npress 'p' to pick up an item \nWhere do you want to go? \npress : \n{GetAvailableRooms(_player.CurrentRoom)}\nor press 0 To Exit the Game.";
                Console.WriteLine(roomsMenu);
                Console.WriteLine($"You are in the {_player.CurrentRoom.Name}");

                // Check if the destination room has a monster
                if (_player.CurrentRoom.HasMonster(new BabyMonsters()))
                {
                    Console.WriteLine("You have a monster in this room. Turn into Attack mode!");
                }
                else
                {
                    // Display the current room information
                    _map.SelfInform();
                }

                string input = Console.ReadLine();
                if (input == "p")
                {
                    Console.WriteLine("Enter the item name you want to pick up:");
                    _player.PickUpItem(Console.ReadLine());
                }
                else if (int.TryParse(input, out int result))
                {
                    if (result == 0)
                    {
                        Console.WriteLine("Thank you for Playing! Have a nice Day.");
                        return;
                    }
                    Room room = FindRoomByID(_map, result);
                    if (room != null)
                    {
                        // Check if the destination room is adjacent to the current room
                        if (Math.Abs(_player.CurrentRoom.X - room.X) + Math.Abs(_player.CurrentRoom.Y - room.Y) == 1)
                        {
                            // If the destination room has a monster, the player cannot move to that room until they eliminate the monster
                            if (_player.CurrentRoom.HasMonster(new BigMonster()))
                            {
                                Console.WriteLine("You have to eliminate the monster in this room before moving on!");
                            }
                            else
                            {
                                // If the destination room requires a key, check if the player has the key
                                if (room.RequiresKey)
                                {
                                    Console.WriteLine($"To open the {room.Name} you need to find {room.KeyRequired}.");

                                    // Check if the player has the required key
                                    if (_player.HasKey(room.KeyRequired))
                                    {
                                        // If the player has the key, remove it from the inventory and move to the destination room
                                        _player.RemoveKey(room.KeyRequired);
                                        _player.CurrentRoom = room;
                                    }
                                    else
                                    {
                                        Console.WriteLine("You don't have the required key to open this door.");
                                    }
                                }
                                else
                                {
                                    // If the destination room does not require a key, simply move to the room
                                    _player.CurrentRoom = room;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid destination. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Room number not found");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a numeric value or 'p' to pick up an item.");
                }
            }

        }


        //public static string GetAvailableRooms(Map map)
        //{
        //    // A helper method to get the IDs of the rooms that are adjacent to the current room
        //    string availableRooms = "";
        //    foreach (Room room in map.Rooms)
        //    {
        //        if (Math.Abs(room.X - _player.CurrentRoom.X) + Math.Abs(room.Y - _player.CurrentRoom.Y) == 1)
        //        {
        //            availableRooms += $"{room.ID} to go to the {room.Name} \n";
        //        }
        //    }
        //    return availableRooms;
        //}
        //public static string GetAvailableRooms(Room currentRoom)
        //{
        //    string availableRooms = "";

        //    foreach (Door door in currentRoom.Doors)
        //    {
        //        availableRooms += $"{door.Name} to go to the {door.Destination.Name} \n";
        //    }

        //    return availableRooms;
        //}
        public static string GetAvailableRooms(Room currentRoom)
        {
            if (currentRoom == null || currentRoom.Doors == null)
            {
                return ""; // Return an empty string if the current room or its doors are null
            }

            string availableRooms = "";
            foreach (Door door in currentRoom.Doors)
            {
                if (door.Destination != null)
                {
                    availableRooms += $"{door.Name} to go to the {door.Destination.Name} \n";
                }
            }

            return availableRooms;
        }




        public static Room FindRoomByID(Map map, int id)
        {
            foreach (Room room in map.Rooms)
            {
                if (room.ID == id)
                {
                    // Check if the room is adjacent to the current room
                    if (Math.Abs(room.X - _player.CurrentRoom.X) + Math.Abs(room.Y - _player.CurrentRoom.Y) == 1)
                    {
                        // Update the current room
                        _player.CurrentRoom = room;
                        return room;
                    }
                }
            }
            return null;
        }
    }
}
