using Rememberall.Domains;
using System;

namespace Rememberall
{
    class Program
    {
        static void Main(string[] args)
        {
            Users.CurrentUserId = 1;
            var engine = new Engine();
            engine.Run();

            



        }
    }
}
