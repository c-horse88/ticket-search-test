namespace TicketSearch.Data
{
    public class Type
    {
        public string Selector { get { return _selector; } }
        private string _selector { get; set; }
        public string Name { get { return _name; } }
        private string _name { get; set; }
        public object Object { get { return _object; } }
        private object _object { get; set; }
        public Type(string selector, string name, object type)
        {
            _selector = selector;
            _name = name;
            _object = type;
        }
    }
}