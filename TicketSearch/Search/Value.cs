using System;
using System.ComponentModel;
using TicketSearch.Functions;
using TicketSearch.Functions.Input;
using TicketSearch.Functions.Results;

namespace TicketSearch.Search
{
    public static class Value
    {
        private static Query _query { get; set; }

        public static void Execute(dynamic input)
        {
            //Get input and call Validation
            _query = input.Data;
            Console.WriteLine($"Please enter search term for field {_query.Field}:\n(Note: leave blank to find all items where the chosen field is blank)");
            Validation.Validate(Console.ReadLine(), Validate, Search.Execute, Execute);
        }

        private static dynamic Validate(dynamic input)
        {
            var descriptor = TypeDescriptor.GetConverter(_query.RequiredType);
            //Use TypeDescriptor to validate input can be converted to correct type for dynamic matching
            //and add converted input to query's term and true return a result to validator
            if (descriptor.IsValid(input))
            {
                _query.Term = descriptor.ConvertFromString(input);
                return new Result(true, _query);
            }
            //for types that don't need casting add input to query's term and return a true result including query to  validator
            else if ((input == "" && _query.RequiredType == typeof(System.String)) || _query.RequiredType == typeof(System.Collections.Generic.List<System.String>))
            {
                _query.Term = input;
                return new Result(true, _query);
            }

            //inform user of invalid input and return false result including query to validator
            Console.WriteLine($"{(input == "" ? "Blank" : input)} is not valid for the type of: {new TypeTranslator(KnownTypes.Types).Translate(_query.RequiredType)}! Press any key to continue!");
            Console.ReadKey();
            return new Result(false, _query);

        }
    }
}
