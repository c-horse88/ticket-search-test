using System;
using System.Collections.Generic;
using TicketSearch.Model;
using TicketSearch.Functions.Results;

namespace TicketSearch.Data
{
    public static class Types
    {

        private static List<Type> _dataTypes = new List<Type>(){
            new Type("1","Users", new User()),
            new Type("2","Tickets", new Ticket()),
            new Type("3","Organizations", new Organization())
        };

        public static List<Type> Get() => _dataTypes;
    }
}