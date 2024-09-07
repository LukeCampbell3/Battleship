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

            foreach (Ship s in ships)
            {
                if (s.IsDead()) // Skip checking a ship if it's already dead
                {
                    continue;
                }

                if (s.CheckHit(attack)) // Only proceed if the attack hit this ship
                {
                    s.TakeDamage(attack);
                    hit = true;

                    if (s.IsDead()) // Check if the ship is dead after taking damage
                    {
                        Console.WriteLine("You have taken out a ship!");
                    }
                }
            }

            if (!hit) // If no ship was hit, display a message
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
            while (true) // Keeps this method happy
            {
                try
                {
                    Console.Write("Enter attack coordinates (x,y): ");
                    string input = Console.ReadLine();

                    // parse user input
                    string[] parts = input.Split(',');

                    // user input handling
                    //if the user inputs more or less than a 2d coord
                    if (parts.Length != 2)
                    {
                        throw new FormatException("Command not recognized. Please use the format 'x,y'.");
                    }

                    // makes sure the parsed x , y coordinates are set
                    int x = int.Parse(parts[0]);
                    int y = int.Parse(parts[1]);

                    // Check if the coordinates are within the valid range
                    if (x < 0 || x >= 11 || y < 0 || y >= 11)
                    {
                        throw new ArgumentOutOfRangeException("Coordinates must be within the range 0-9.");
                    }

                    // converts x , y to coord2d
                    return new Coord2D(x, y);
                }
                //yay exceptions
                catch (FormatException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
                // let user try to input again
                Console.WriteLine("Please try again."); 
            }
        }


    }
}
