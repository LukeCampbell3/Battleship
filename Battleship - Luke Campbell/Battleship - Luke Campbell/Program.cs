using System.Threading.Channels;

namespace Battleship___Luke_Campbell
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //init\
            Menu menu = new Menu();
            //Ship[] ships;
            List<string> descriptions = new List<string>();

            // creates ships based on file path and non-file path inputs
            menu.ShipInit(descriptions);

            menu.MainLoop();

        }
    }
}

