using System;

namespace CombatEffective
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (CombatEffective game = new CombatEffective())
            {
                game.Run();
            }
        }
    }
}

