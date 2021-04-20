using System;
using System.Collections.Generic;
using TicketSearch.Functions;

namespace TicketSearch.Menu
{
    public class Options
    {
        private static List<Option> _options = new List<Option>(){
            new Option("1","perform a Search", Search.Menu.Execute),
            new Option("2","list data types and fields available to search",Fields.Execute),
            new Option("quit","exit the utility", o =>{} )
        };
        public static List<Option> Get() => _options;
    }
}
