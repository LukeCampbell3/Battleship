using System.Threading.Channels;

namespace Battleship___Luke_Campbell
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //init menu object so Main looks pretty
            Menu menu = new Menu();

            // this is my loop that takes user's filepaths
            menu.ShipInit();

            // this is the main game logic for attacking or checking ship status
            menu.MainLoop();

        }
    }
}

