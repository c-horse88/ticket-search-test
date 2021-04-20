using System;
namespace TicketSearch.Functions.Input
{
    public static class Validation
    {
        //used to validate user input before moving on to the next action or returning to the appropriate action when invalid
        public static void Validate(dynamic input, Func<dynamic, dynamic> validator, Action<dynamic> successFunction, Action<dynamic> failureFunction)
        {
            var result = validator(input);
            if (result.Success == true) successFunction(result);
            else failureFunction(result);
        }
    }
}