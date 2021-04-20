using System;
using System.Collections.Generic;
using Xunit;
using TicketSearch.Model;
namespace TicketSearch.Test.Functions.Results
{
    public class Formatter
    {

        [Fact]
        public void Format_GivenUserObject_ThreeSpacesMatchesExpectedString()
        {
            //Given
            var obj = new { id = 1, child = new List<string>() { "bob" }, parent = "Mary" };

            //When
            var formatted = TicketSearch.Functions.Results.Formatter.Format(obj, 2);

            //Then
            var anticipated = "<>f__AnonymousType2`3:\n[\n  id: 1\n  child:\n  [\n    bob\n  ]\n  parent: Mary\n]\n";



            Assert.Equal(anticipated, formatted);
        }
    }
}