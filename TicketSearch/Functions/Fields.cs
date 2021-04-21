using System;
using System.Linq;
using System.Collections.Generic;

using TicketSearch.Menu;

namespace TicketSearch.Functions
{
    public static class Fields
    {

        public static void Execute(dynamic input)
        {
            List();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Main.Execute(Menu.Options.Get());
        }

        private static void List()
        {
            foreach (var dataType in Data.Types.Get())
            {
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine($"Search {dataType.Name} using:\n");
                Console.WriteLine(String.Format("{0,-30}{1,-20}\n", "Field Name", "Data Type"));
                foreach (var prop in dataType.Object.GetType().GetProperties())
                {
                    Console.WriteLine(String.Format("{0,-30}{1,-20}", prop.Name, new TypeTranslator(KnownTypes.Types).Translate(prop.PropertyType)));
                }
                Console.WriteLine("\n");
            }
        }
    }
}
