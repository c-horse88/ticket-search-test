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
            if (results.Count == 0)
            {
                Console.WriteLine($"No matching value in {query.DataTypeName} when searching for {(query.Term != null ? query.Term?.ToString() : "empty cases of")} as a value for  {query.Field}\n");
            }
            else
            {
                Formatter.Format(results, 5);
            }

            //return to main menu
            Console.WriteLine("Press any key to return to the menu");
            Console.ReadKey();
            Main.Execute(Options.Get());
        }
    }
}