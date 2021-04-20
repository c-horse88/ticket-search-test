using System;
using TicketSearch.Menu;
using TicketSearch.Functions.Results;


namespace TicketSearch.Search
{
    public static class Search
    {

        public static void Execute(dynamic input)
        {
            //take query from input and execute its function
            Query query = input.Data;
            dynamic results = query.Execute();

            //check if results are null or an empty list before outputing search result
            if (results == null || results.Count == 0)
            {
                Console.WriteLine($"No matching {query.Field} in {query.DataTypeName} when searching for {(query.Term.ToString() != "" ? query.Term.ToString() : "empty cases of")} {query.Field}");
            }
            else
            {
                Console.WriteLine(Formatter.Format(results, 5));
            }

            //return to main menu
            Console.WriteLine("Press any key to return to the menu");
            Console.ReadKey();
            Main.Execute();
        }
    }
}