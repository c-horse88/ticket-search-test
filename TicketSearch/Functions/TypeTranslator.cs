using System;
using System.Linq;
using System.Collections.Generic;
namespace TicketSearch.Functions
{
    public class TypeTranslator
    {
        public Dictionary<Type, string> Types { get { return _types; } }
        private Dictionary<Type, string> _types { get; set; }
        public TypeTranslator(Dictionary<Type, string> types)
        {
            _types = types;
        }
        public string Translate(Type prop)
        {
            return _types.FirstOrDefault(field => field.Key == prop).Value ?? prop.Name;
        }
    }
}