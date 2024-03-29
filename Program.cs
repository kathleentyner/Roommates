﻿using Roommates.Models;
using Roommates.Repositories;
using System;
using System.Collections.Generic;

namespace Roommates
{
    class Program
    {

        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true; TrustServerCertificate=True;";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for roommate by id"):
                        Console.Write("Roommate Id: ");
                        int Id = int.Parse(Console.ReadLine());
                       
                        Roommate roommate = roommateRepo.GetById(Id);

                        Console.WriteLine($"{roommate.Id} - {roommate.FirstName} room {roommate.Room.Name})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show roommates"):
                        List<Roommate> roommates = roommateRepo.GetAll();
                        foreach (Roommate rm in roommates)
                        {
                            Console.WriteLine($"{rm.FirstName} {rm.LastName} has an Id of {rm.Id}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Show all chores"):
                        List<Chore>chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an Id of {c.Id}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey(); //looks for what the user does
                        break;
                    case ("Search for a chore"):
                        Console.Write("Chore Id: ");
                        int choreid = int.Parse(Console.ReadLine());
                        Chore chore = choreRepo.GetById(choreid);
                        Console.WriteLine($"{chore.Id} - {chore.Name})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a chore"):
                        Console.Write("Chore: ");
                        string choreName = Console.ReadLine();
                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName,
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }
            static string GetMenuSelection()
            {
                Console.Clear();

                List<string> options = new List<string>() //Menu of options
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Show roomates",
                "Search for roommate by id",
                "Show all chores",
                "Search for a chore",
                "Add a chore",
                "Exit"
            };

                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {options[i]}");
                }

                while (true)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Select an option > ");

                        string input = Console.ReadLine();
                        int index = int.Parse(input) - 1;
                        return options[index];
                    }
                    catch (Exception)
                    {

                        continue;
                    }
                }
            }
        }
    }
}