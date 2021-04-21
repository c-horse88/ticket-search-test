using System;
using System.Linq;
using System.Collections.Generic;
using TicketSearch.Data;
using TicketSearch.Functions.Input;
using TicketSearch.Functions.Results;



namespace TicketSearch.Search
{
    public static class Menu
    {
        public static void Execute(dynamic input)
        {


            Console.Clear();
            Console.WriteLine("Please select one of the following data types to perform search:");
            foreach (var type in Types.Get())
            {
                Console.WriteLine($"\t - Select {type.Selector}) for {type.Name}");
            }

            Validation.Validate(Console.ReadLine(), Validate, Field.Execute, Execute);

        }
        private static dynamic Validate(dynamic input)
        {

            if (Types.Get().Any(type => type.Selector == input) != true)
            {
                Console.WriteLine("Please select a valid option. Press any key to continue!");
                Console.ReadKey();
                return new Result(false);
            }
            var query = new Query(Data.Types.Get());

            query.DataTypeId = Int32.Parse(input);
            return new Result(true, query);
        }
    }
}