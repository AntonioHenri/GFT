using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteGFT.Domain;

namespace TesteGFT
{
    class Program
    {



        static void Main(string[] args)
        {

            var context = new OrderContext();

            Console.WriteLine("Por favor ingresse o menu .");

            var valueToInput = Console.ReadLine();

            var menuResponse = context.CheckOrder(valueToInput);

            Console.WriteLine(menuResponse);

            Console.ReadKey();

        }
    }
}
