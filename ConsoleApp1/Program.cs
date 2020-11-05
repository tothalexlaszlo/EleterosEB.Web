using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EleterosEB.Data;

namespace ConsoleApp1
{
    class Program
    {
        private static EleterosEBContext context = new EleterosEBContext();
        static void Main(string[] args)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Console.Write("Press any key...");
            Console.ReadKey();
        }
    }
}
