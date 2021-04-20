using System;
using System.Collections.Generic;
using Xunit;
using TicketSearch.Functions;
namespace TicketSearch.Test.Functions
{

    public class TypeTranslator
    {
        [Fact]
        public void Translate_ProvidedUnknownType_ReturnsTypeName()
        {
            //Given
            var translator = new TicketSearch.Functions.TypeTranslator(
            new Dictionary<Type, string>(){
            {typeof(System.String),"Text"},
            {typeof(System.Int32),"Number"},
            {typeof(System.Boolean),"True/False"},
            {typeof(System.Collections.Generic.List<System.String>),"List of text"},
            {typeof(Nullable<DateTime>),"Date"}
            });

            //When
            var test = translator.Translate(typeof(System.DateTime));

            //Then
            Assert.Equal("DateTime", test);
        }
        [Fact]
        public void Translate_ProvidedKnownType_ReturnsRelevantDescription()
        {
            //Given
            var translator = new TicketSearch.Functions.TypeTranslator(
            new Dictionary<Type, string>(){
            {typeof(System.String),"Text"},
            {typeof(System.Int32),"Number"},
            {typeof(System.Boolean),"True/False"},
            {typeof(System.Collections.Generic.List<System.String>),"List of text"},
            {typeof(Nullable<DateTime>),"Date"}
            });

            //When
            var test = translator.Translate(typeof(System.Boolean));

            //Then
            Assert.Equal("True/False", test);
        }
    }
}