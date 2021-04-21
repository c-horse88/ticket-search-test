using System;
using System.Linq;
using System.Collections.Generic;
using TicketSearch.Functions.Input;
using TicketSearch.Functions.Results;

namespace TicketSearch.Menu
{
    public static class Main
    {
        private static List<Option> _options { get; set; }
        public static void Execute(dynamic input)
        {
            _options = input;
            Console.Clear();
            Console.WriteLine("Please select on of the following actions to perform:");
            foreach (var option in _options) Console.WriteLine($"\t - Enter {option.Selector} to {option.Description}.");
            var optionSelected = Console.ReadLine();
            var successFunction = _options.First(option => option.Selector == optionSelected).Action;
            Validation.Validate(optionSelected, Validate, successFunction, Execute);

        }
        private static dynamic Validate(dynamic input)
        {
            if (_options.Exists(opt => opt.Selector == input) != true)
            {
                Console.WriteLine("Please select a valid option. Press any key to continue!");
                Console.ReadKey();
                return new Result(false);
            }
            return new Result(true);
        }
    }
}