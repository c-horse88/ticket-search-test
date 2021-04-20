using System;
using System.Collections.Generic;
namespace TicketSearch.Functions
{
    public static class KnownTypes
    {
        public static Dictionary<Type, string> Types = new Dictionary<Type, string>(){
            {typeof(System.String),"Text"},
            {typeof(System.Int32),"Number"},
            {typeof(System.Boolean),"True/False"},
            {typeof(System.Collections.Generic.List<System.String>),"List of text"},
            {typeof(Nullable<DateTime>),"Date"}
    };
    }
}