using System;
namespace TicketSearch.Menu
{
    public class Option
    {
        public string Selector { get { return _selector; } }
        private string _selector { get; set; }
        public string Description { get { return _description; } }
        private string _description { get; set; }
        public Action<dynamic> Action { get { return _action; } }
        private Action<dynamic> _action { get; set; }
        public Option(string selector, string description, Action<dynamic> action)
        {
            _selector = selector;
            _description = description;
            _action = action;
        }


    }
}