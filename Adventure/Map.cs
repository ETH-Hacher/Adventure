using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Adventure
{
    public class Map
    {
        public List<Room> Rooms { get; set; }
        public Room currentRoom { get; set; }


        public Map()
        {
            //public Room(int id, string name, string description, List<Door> doors, List<string> items, int x, int y, List<Weapons> weapons = null)


            Rooms = new List<Room>();

            List<Door> doorsRoom1 = new List<Door>()
            {
                new Door("Door Nr.02", DoorDirection.Up, true),
                new Door("Door Nr.01", DoorDirection.Down, false)
            };
            List<string> itemsRoom1 = new List<string>();
            Room room1 = new Room(id: 1, 
                                  name: "Main Key Room",
                                  description: "you have ONE Door and TWO Closets.\nTo open the Norhtern Door you need to find Key Nr. 1.",
                                  doors: doorsRoom1, 
                                  items: itemsRoom1,
                                  x: 0, 
                                  y: 0);

            
            currentRoom = room1;
            Keys key1 = new Keys("Key Nr.01", "Door Nr.02");
            room1.Keys.Add(key1);
            Rooms.Add(room1);
            //___________________________________________________________


            List<Door> doorsRoom2 = new List<Door>()
            {
                new Door("Door Nr.03", DoorDirection.Right, true),
                new Door("Door Nr.02", DoorDirection.Down, true)
            };
            List<string> itemsRoom2 = new List<string>();
            itemsRoom2.Add("item1");
            List<Weapons> weaponsRoom2 = new List<Weapons>();
            List<Keys> keysRoom2 = new List<Keys>();

            Room room2 = new Room(id: 2,
                                  name: "First Weapons Room",
                                  description: "you have TWO Doors, One Closets, a Table and a Chair.\nYou can take some Weapons and Ammunition.\nTo open the Eastern Door you need to find Key Nr. 2.",
                                  doors: doorsRoom2,
                                  items: itemsRoom2,
                                  x: 1,
                                  y: 0, 
                                  weapons: weaponsRoom2);
            currentRoom = room2;
            Keys key2 = new Keys("Key Nr.02", "Door Nr.03");
            room2.Keys.Add(key2);
            room2.Weapons.Add(new Gun());
            room2.Weapons.Add(new Sword());
            room2.Weapons.Add(new Bow());
            Rooms.Add(room2);
            //___________________________________________________________


            List<Door> doorsRoom3 = new List<Door>()
            {
                new Door("Door Nr.03", DoorDirection.Left, true),
                new Door("Door Nr.04", DoorDirection.Up, true)
            };
            List<string> itemsRoom3 = new List<string>();
            List<Weapons> weaponsRoom3 = new List<Weapons>();
            List<Keys> keysRoom3 = new List<Keys>();




            Room room3 = new Room(id: 3,
                                  name: "First BabyMonsters Room",
                                  description: "you have TWO Doors and Two Closet.\nYou also have Three little BabyMonsters that you have to eliminate to go to the next Room.\nTo open the Northern Door you need to find Key Nr. 3.",
                                  doors: doorsRoom3,
                                  items: itemsRoom3,
                                  x: 1,
                                  y: 1,
                                  weapons: weaponsRoom3);

            currentRoom = room3;
            //room3.HasMonster(Monsters, BabyMonsters);
            room3.Monster = new BabyMonsters();
            Keys key3 = new Keys("Key Nr.03", "Door Nr.04");
            room3.Keys.Add(key3);
            Rooms.Add(room3);
            //___________________________________________________________


            List<Door> doorsRoom4 = new List<Door>()
            {
                new Door("Door Nr.04", DoorDirection.Down, true),
                new Door("Door Nr.05", DoorDirection.Right, true)
            };
            List<string> itemsRoom4 = new List<string>();
            List<Weapons> weaponsRoom4 = new List<Weapons>();
            List<Powers> powersRoom4 = new List<Powers>()
            {
                new Powers("PowerSingle", 100),
                new Powers("PowerDouble", 200),
                new Powers("PowerSingle", 100),
                new Powers("PowerDouble", 200),
                new Powers("PowerSingle", 100),
                new Powers("PowerDouble", 200),

            };
            List<Keys> keysRoom4 = new List<Keys>();

            Room room4 = new Room(id: 4,
                                  name: "First Refreshing Room",
                                  description: "you have TWO Doors, a Table and Three Closet.\nYou can take some Power Refill.\nTo open the Eastern Door you need to find Key Nr. 4.",
                                  doors: doorsRoom4,
                                  items: itemsRoom4,
                                  x: 1,
                                  y: 2,
                                  powers: powersRoom4,
                                  weapons: weaponsRoom4);

            currentRoom = room4;
            Keys key4 = new Keys("Key Nr.04", "Door Nr.05");
            room4.Keys.Add(key4);
            Rooms.Add(room4);
            //___________________________________________________________


            List<Door> doorsRoom5 = new List<Door>()
            {
                new Door("Door Nr.05", DoorDirection.Left, true),
                new Door("Door Nr.06", DoorDirection.Right, true)
            };
            List<string> itemsRoom5 = new List<string>();
            List<Weapons> weaponsRoom5 = new List<Weapons>();
            List<Keys> keysRoom5 = new List<Keys>();

            Room room5 = new Room(id: 5,
                                  name: "Second Weapons Room",
                                  description: "you have TWO Doors, One Closets, a Table and a Chair.\nYou can take some Weapons and Ammunition.\nTo open the Eastern Door you need to find Key Nr. 5.",
                                  doors: doorsRoom5,
                                  items: itemsRoom5,
                                  x: 2,
                                  y: 2,
                                  weapons: weaponsRoom5);

            currentRoom = room5;
            Keys key5 = new Keys("Key Nr.05", "Door Nr.06");
            room5.Keys.Add(key5);
            Rooms.Add(room5);
            //___________________________________________________________


            List<Door> doorsRoom6 = new List<Door>()
            {
                new Door("Door Nr.06", DoorDirection.Left, true),
                new Door("Door Nr.07", DoorDirection.Up, true)
            };
            List<string> itemsRoom6 = new List<string>();
            List<Weapons> weaponsRoom6 = new List<Weapons>();
            List<Keys> keysRoom6 = new List<Keys>();

            Room room6 = new Room(id: 6,
                                  name: "Second BabyMonsters Room",
                                  description: "you have TWO Doors and Two Closet.\nYou also have Five little BabyMonsters that you have to eliminate to go to the next Room.\nTo open the Northern Door you need to find Key Nr. 6.",
                                  doors: doorsRoom6,
                                  items: itemsRoom6,
                                  x: 3,
                                  y: 2,
                                  weapons: weaponsRoom6);

            currentRoom = room6;
            Keys key6 = new Keys("Key Nr.06", "Door Nr.07");
            room6.Keys.Add(key6);
            Rooms.Add(room6);
            //___________________________________________________________


            List<Door> doorsRoom7 = new List<Door>()
            {
                new Door("Door Nr.07", DoorDirection.Down, true),
                new Door("Door Nr.08", DoorDirection.Up, true)
            };
            List<string> itemsRoom7 = new List<string>();
            List<Weapons> weaponsRoom7 = new List<Weapons>();
            List<Keys> keysRoom7 = new List<Keys>();

            Room room7 = new Room(id: 7,
                                  name: "Ultimate Weapons Room",
                                  description: "you have TWO Doors, Three Closets, a Table and a Big Box.\nYou can take some Weapons and Ammunition.\nTo open the Northern Door you need to find Key Nr. 7.",
                                  doors: doorsRoom7,
                                  items: itemsRoom7,
                                  x: 3,
                                  y: 3,
                                  weapons: weaponsRoom7);

            currentRoom = room7;
            Keys key7 = new Keys("Key Nr.07", "Door Nr.08");
            room7.Keys.Add(key7);
            Rooms.Add(room7);
            //___________________________________________________________


            List<Door> doorsRoom8 = new List<Door>()
            {
                new Door("Door Nr.08", DoorDirection.Down, true),
                new Door("Door Nr.09", DoorDirection.Left, true)
            };
            List<string> itemsRoom8 = new List<string>();
            List<Weapons> weaponsRoom8 = new List<Weapons>();
            List<Keys> keysRoom8 = new List<Keys>();

            Room room8 = new Room(id: 8,
                                  name: "Second Refreshing Room",
                                  description: "you have TWO Doors and Four Closet.\nYou can take some Power Refill to prepare yourself to the BIG FIGHT.\nTo open the Weastern Door you need to find Key Nr. 8.",
                                  doors: doorsRoom8,
                                  items: itemsRoom8,
                                  x: 3,
                                  y: 4,
                                  weapons: weaponsRoom8);

            currentRoom = room8;
            Keys key8 = new Keys("Key Nr.08", "Door Nr.09");
            room8.Keys.Add(key8);
            Rooms.Add(room8);
            //___________________________________________________________


            List<Door> doorsRoom9 = new List<Door>()
            {
                new Door("Door Nr.09", DoorDirection.Right, true),
                new Door("Door Nr.10", DoorDirection.Left, true)
            };
            List<string> itemsRoom9 = new List<string>();
            List<Weapons> weaponsRoom9 = new List<Weapons>();
            List<Keys> keysRoom9 = new List<Keys>();

            Room room9 = new Room(id: 9,
                                  name: "MONESTER Room",
                                  description: "you have TWO Doors.\nYou have a BIG MONSTER that you have to eliminate in order to WIN the GAME.\nTo open the Northern Door you need Key Nr. 9 which you ONLY can get when you eliminate the BIG MONSTER",
                                  doors: doorsRoom9,
                                  items: itemsRoom9,
                                  x: 2,
                                  y: 4,
                                  weapons: weaponsRoom9);

            currentRoom = room9;
            Keys key9 = new Keys("Key Nr.09", "Door Nr.10");
            room9.Keys.Add(key9);
            Rooms.Add(room9);
            //________________________________________________________________________________________
        }

        public void SelfInform()
        {
            Console.WriteLine("You are in the " + currentRoom.Name);
            Console.WriteLine(currentRoom.Description);
            Console.WriteLine("Available doors:");
            foreach (var door in currentRoom.Doors)
            {
                Console.WriteLine($"- {door.Name}");
            }
        }
    }
}
