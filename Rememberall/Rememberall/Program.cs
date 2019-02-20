using Rememberall.Domains;
using System;

namespace Rememberall
{
   public class Program
    {
        static void Main(string[] args)
        {


            Users.CurrentUserId = null;
            Users.ResettedUserint = 0;
            var engine = new Engine();
            var loader = new Loader();
            Loader.Run();
            engine.Run();

            

        }
    }
}
