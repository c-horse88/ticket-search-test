using System;
using TicketSearch.Menu;
using TicketSearch.Functions.Input;
using TicketSearch.Functions.Results;

namespace TicketSearch
{
    public static class Introduction
    {
        public static void Execute(dynamic input = null)
        {
            Console.WriteLine("Welcome to the Ticket Search System");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Menu.Main.Execute(Menu.Options.Get());
        }

    }
}