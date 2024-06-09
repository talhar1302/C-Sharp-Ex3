using GameLogic;

namespace GameUI
{
    class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI();
            GameRunner gameRunner = new GameRunner(ui);
            gameRunner.Run();
        }
    }
}
