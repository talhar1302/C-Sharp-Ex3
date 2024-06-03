using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUI
{
    class Program
    {
        public static void Main(string[] args)
        {
            UI ui = new UI();
            Game game = new Game(ui);
            game.Play();
        }
    }

}
