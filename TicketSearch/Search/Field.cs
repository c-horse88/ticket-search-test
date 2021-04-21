using System;
using System.Linq;
using System.Collections.Generic;
using TicketSearch.Functions.Input;
using TicketSearch.Functions.Results;

namespace TicketSearch.Search
{
    //Gets and validates users input when selecting a field to complete the search using the list of Data Types
    public static class Field
    {
        private static List<Data.Type> _types = Data.Types.Get();
        private static Query _query { get; set; }

        public static void Execute(dynamic input)
        {
            _query = input.Data;
            Console.WriteLine($"Please select a field for the data type {_types.FirstOrDefault(type => Int32.Parse(type.Selector) == _query.DataTypeId).Name}:");
            Validation.Validate(Console.ReadLine(), Validate, Value.Execute, Execute);
        }
        private static dynamic Validate(dynamic input)
        {
            //get list of fields based on the query's Data Type to validate users choice
            var fields = _types.FirstOrDefault(type => Int32.Parse(type.Selector) == _query.DataTypeId).Object.GetType().GetProperties();

            //check input matches any fieldname
            if (!fields.Any(fields => fields.Name == input))
            {
                Console.WriteLine("Please select a valid search field. Press any key to continue!");
                Console.ReadKey();
                return new Result(false, _query);
            }
            // Add input search type and it's required type to query before returning to successfunction
            _query.Field = input;
            _query.RequiredType = fields.First(fields => fields.Name == input).PropertyType;
            return new Result(true, _query);
        }
    }
}
