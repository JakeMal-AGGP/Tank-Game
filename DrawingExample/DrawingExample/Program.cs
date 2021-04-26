using System;

namespace DrawingExample
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        ///

        public static bool restart;

        [STAThread]
        static void Main()
        {
            //using (var game = new GameMode())
            //game.Run();

            do
            {
                Program.restart = false;
                var game = new GameMode();
                game.Run();
            }
            while (Program.restart);
        }
    }
#endif
}
