using System;

namespace Physics_Sim
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Sim())
                game.Run();
        }
    }
}
