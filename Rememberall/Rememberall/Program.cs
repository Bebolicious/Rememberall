using Rememberall.Domains;
using System;

namespace Rememberall
{
    class Program
    {
        static void Main(string[] args)
        {
            Users.CurrentUserId = null;
            var engine = new Engine();
            engine.Run();

            



        }
    }
}
