using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{

    // may or may not use IDK
    internal class Menu
    {
        public Ship[] ships;

        /// <summary>
        /// initializes file input and parses the file to create ships
        /// </summary>
        /// <returns>array of ships that have been created</returns>
        public Ship[] ShipInit()
        {
            while (true) // Keep asking until we get a valid input
            {
                Console.WriteLine("Please enter the file path to your .csv which contains your ships:");
                string filepath = Console.ReadLine();

                ships = ShipFactory.ParseShipFile(filepath); // Try to parse the file

                if (ships != null && ships.Length > 0) // Check if the parsing was successful
                {
                    return ships; // Return the valid ships
                }
                else
                {
                    Console.WriteLine("Invalid file or no ships found. Please try again."); // Error message
                }
            }
        }

        /// <summary>
        /// this is the main game loop
        /// this method handles attack input along with status checking
        /// </summary>
        public void MainLoop()
        {
            bool playAgain = true;
            Coord2D attack = new Coord2D();
            // store a list of previous attacks for the user to see rather than where the ships are

            while (playAgain)
            {
                bool gameOver = false;

                while (!gameOver)
                {
                    int choice;
                    Console.WriteLine("1 = attack\n2 = see status");

                    while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
                    {
                        Console.WriteLine("Invalid choice. Please select a valid option (1 [ attack ] or 2 [ see status ]).");
                    }

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("You chose ATTACK\n\tWhere would you like to attack? [please use (x,y) format :) ]");

                            /// takes the user inputted coords and passes them to <see cref="GetCoordFromUser"/>
                            attack = GetCoordFromUser();
                            // passes the coord to this method that checks if it hit a ship
                            Userattack(attack, ships);

                            break;
                        case 2:
                            Console.WriteLine("Here is the status of your ships:");
                            // gets info for each CREATED ship
                            foreach (var ship in ships)
                            {
                                Console.WriteLine(ship.GetInfo());
                            }
                            break;
                    }

                    // Check if all ships are dead
                    gameOver = ships.All(s => s.IsDead());

                    if (gameOver)
                    {
                        Console.WriteLine("All ships have been sunk! Game over!");
                    }
                }

                Console.WriteLine("Do you want to play again? (yes/no)");
                string response = Console.ReadLine().ToLower();
                playAgain = response == "yes";
            }

            Console.WriteLine("Thank you for playing!");
        }

        /// <summary>
        /// this method handles the user's attacks
        /// </summary>
        /// <param name="attack"><see cref="Coord2D"/>this is a coordinate point the user inputs</param>
        /// <param name="ships">this is the list of ships that need checked</param>
        public void Userattack(Coord2D attack, Ship[] ships)
        {
            bool hit = false; // Track if a hit has occurred
            bool alreadyHit = false; // Track if the coordinate was already attacked

            foreach (Ship s in ships)
            {
                if (s.IsDead()) // Skip checking a ship if it's already dead
                {
                    continue;
                }

                if (s.CheckHit(attack)) // Only proceed if the attack hit this ship
                {
                    hit = true;

                    if (s.IsDead()) // Check if the ship is dead after taking damage
                    {
                        Console.WriteLine("You have taken out a ship!");
                    }
                }
                else if (s.DamagedPoints.Contains(attack))
                {
                    alreadyHit = true; // Mark as already hit
                }
            }

            if (!hit && !alreadyHit) // If no ship was hit and it was not a repeated attack
            {
                Console.WriteLine("You missed...");
            }
        }



        /// <summary>
        /// This takes the user's inputted coordinates that are attacks
        /// </summary>
        /// <returns>returns a coord2d that is to be used to check if the attack hit a ship</returns>
        public static Coord2D GetCoordFromUser()
        {
            // uses bool logic so users can stay inside the loop
            bool isValidInput = false;
            // creates an instance of a Coord2D so it can be set later in the loop
            Coord2D coord = new Coord2D();  

            do
            {
                try
                {
                    Console.Write("Enter attack coordinates (x,y): ");
                    string input = Console.ReadLine();

                    // parse user input
                    string[] parts = input.Split(',');

                    // Check if the input has exactly two parts
                    if (parts.Length != 2)
                    {
                        throw new FormatException("Command not recognized. Please use the format 'x,y'.");
                    }

                    // Parse x and y coordinates
                    int x = int.Parse(parts[0]);
                    int y = int.Parse(parts[1]);

                    // Check if the coordinates are within the valid range
                    if (x < 0 || x >= 11 || y < 0 || y >= 11)
                    {
                        throw new ArgumentOutOfRangeException("Coordinates must be within the range 0-9.");
                    }

                    // If input is valid, set isValidInput to true and return coordinates
                    coord = new Coord2D(x, y);
                    isValidInput = true;
                }
                catch (FormatException ex)                          // if the input is invalid input
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (ArgumentOutOfRangeException ex)              // if coords are outside bounds
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)                               // if any application errors were to occur 
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }

                if (!isValidInput)                                 // a catch all output
                {
                    Console.WriteLine("Please try again.");
                }
            }
            while (!isValidInput);                                 // Keeps loop going even though input is invalid

            return coord;                                          // returns the created Coord2D
        }
    }
}
