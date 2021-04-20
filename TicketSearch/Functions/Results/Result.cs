namespace TicketSearch.Functions.Results
{
    struct Result
    {
        public bool Success;
        public object Data;
        public Result(bool success, object data = null)
        {
            Success = success;
            Data = data;
        }
    }
}