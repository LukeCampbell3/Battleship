using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{

    // may or may not use IDK
    internal class Menu
    {
        public Ship[] ships;

        public Ship[] ShipInit(List<string> descriptions)
        {
            while (true)
            {
                Console.WriteLine("Do you have a .csv of pre-made ships (Y/N)?");
                string fileInput = Console.ReadLine();

                if (fileInput.ToLower() == "y")
                {
                    Console.WriteLine("Please enter the file path to your .csv which contains your ships:");
                    string filepath = Console.ReadLine();

                    ships = ShipFactory.ParseShipFile(filepath); // Assign directly to the class-level array

                    return ships;
                }
                else if (fileInput.ToLower() == "n")
                {
                    //game logic
                    foreach (string description in descriptions)
                    {
                        Ship ship = ShipFactory.ParseShipString(description);
                        ships.Append(ship);
                    }
                }
                else
                {
                    Console.WriteLine("Please follow the format (Y/N)...");
                    return null;
                }
            }
        }

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
                            Console.WriteLine("You chose ATTACK\nWhere would you like to attack? [please use (x,y) format :) ]");

                            attack = GetCoordFromUser();
                            Userattack(attack, ships);

                            break;
                        case 2:
                            //dont get info 
                            //let the user access previous attacks to "strategize"
                            Console.WriteLine("Here is the status of your ships:");
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


        public void Userattack(Coord2D attack, Ship[] ships)
        {
            foreach (Ship s in ships)
            {
                s.TakeDamage(attack);
                if (s.IsDead()) // Check if the ship is dead after taking damage
                {
                    Console.WriteLine("You have taken out a ship!");
                }
                break; // Only hit one ship per attack
            }
        }


        public static Coord2D GetCoordFromUser()
        {
            while (true) // Loop until valid input is provided
            {
                try
                {
                    Console.Write("Enter attack coordinates (x,y): ");
                    string input = Console.ReadLine();

                    // Step 2: Parse the Input
                    string[] parts = input.Split(',');

                    if (parts.Length != 2)
                    {
                        throw new FormatException("Invalid format. Please use the format 'x,y'.");
                    }

                    int x = int.Parse(parts[0]);
                    int y = int.Parse(parts[1]);

                    // Check if the coordinates are within the valid range
                    if (x < 0 || x > 9 || y < 0 || y > 9)
                    {
                        throw new ArgumentOutOfRangeException("Coordinates must be within the range 0-9.");
                    }

                    // Step 3: Convert to Coord2D
                    return new Coord2D(x, y);
                }
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

                Console.WriteLine("Please try again."); // Prompt user to try again
            }
        }


    }
}
