using System;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Data;
using System.Collections.Generic;

using Xunit;
using TicketSearch.Data;
using TicketSearch.Model;
namespace TicketSearch.Test
{
    public class Bank_Test
    {
        Bank _bank;
        List<object> _testListObject;
        List<Data.Type> _types;

        public Bank_Test()
        {
            _types = new List<Data.Type>()
            {
                new Data.Type("1","Users", new User()),
                new Data.Type("2","Tickets", new Ticket()),
                new Data.Type("3","Organizations", new Organization())
            };
            _testListObject = new List<object>()
            {
                new { stringValue ="value_1", intValue = 1, dateValue = new DateTime(1,1,1), testObject = new {objValue = "i'm the first object"}},
                new { stringValue ="value_2", intValue = 2, dateValue = new DateTime(2,2,2), testObject = new {objValue = "i'm the second object"}},
                new { stringValue ="value_3", intValue = 3, dateValue = new DateTime(2,2,2), testObject = new {objValue = "i'm the third object"}},
                new { stringValue ="value_4", intValue = 4, dateValue = new DateTime(2,2,2), testObject = new {objValue = "i'm the fourth object"}}
            };
            _bank = new Bank(_types);
        }

        [Fact]
        public void CheckDefaultPathIsValid()
        {
            //When
            var path = typeof(Bank).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic).First(prop => prop.Name == "_path").GetValue(_bank);
            //Then
            Assert.Equal(Path.Combine(Environment.CurrentDirectory, "Files/JSON"), path);

        }
        [Fact]
        public void CheckPathUpdates()
        {
            //Given
            _bank = new Bank(Data.Types.Get(), "test");
            //When
            var path = typeof(Bank).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic).First(prop => prop.Name == "_path").GetValue(_bank);
            //Then
            Assert.NotEqual(Path.Combine(Environment.CurrentDirectory, "Files/JSON"), path);
            Assert.Equal("test", path);

        }
        [Fact]
        public void ConvertToDataTable_TestListObject_ColumnCountShouldMatch()
        {
            //When
            DataTable table = (DataTable)typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                                    .First(method => method.Name == "ConvertToDataTable")
                                    .Invoke(_bank, new[] { _testListObject });
            //Then
            Assert.Equal(_testListObject.First().GetType().GetProperties().Length, table.Columns.Count);
        }
        [Fact]
        public void ConvertToDataTable_TestListObject_RowCountShouldMatch()
        {
            //When
            DataTable table = (DataTable)typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                                            .First(method => method.Name == "ConvertToDataTable")
                                            .Invoke(_bank, new[] { _testListObject });
            //Then
            Assert.Equal(_testListObject.Count, table.Rows.Count);
        }
        [Fact]
        public void ConvertToDataTable_TestListObject_ColumnNamesMatch()
        {
            //Given
            var names = new string[] { "stringValue", "intValue", "dateValue", "testObject" };
            //When
            DataTable table = (DataTable)typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                                            .First(method => method.Name == "ConvertToDataTable")
                                            .Invoke(_bank, new[] { _testListObject });
            //Then

            Assert.True(table.Columns.Cast<DataColumn>().All(col => names.Contains(col.ColumnName)));
        }
        [Fact]
        public void ConvertToDataTable_TestListObject_ColumnTypesMatch()
        {
            //Given
            var types = new string[] { "String", "Int32", "DateTime", "AnonymousType" };
            //When
            DataTable table = (DataTable)typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                                            .First(method => method.Name == "ConvertToDataTable")
                                            .Invoke(_bank, new[] { _testListObject });
            //Then
            Assert.True(table.Columns.Cast<DataColumn>().All(col => types.Any(type => col.DataType.Name.Contains(type))));
        }
        [Fact]
        public void ConvertToDataTable_EmptyListObject_ThrowsException()
        {
            //Given
            var testEmptyListObject = new List<object>();

            //When
            try
            {
                Assert.Throws<System.Exception>(() =>
                {
                    return (DataTable)typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                        .First(method => method.Name == "ConvertToDataTable")
                        .Invoke(_bank, new[] { testEmptyListObject });
                });
            }
            catch (System.Exception ex)
            {
                //Then find the exception thrown by ConvertToDataTable;
                Assert.Equal("IEnumerable<dynamic> data contains has a count of 0. parameters must provide a IEnumerable with a non zero count", ex.InnerException.InnerException.Message);
            }
        }
        [Fact]

        public void LoadDataSources_ReturnsActionType()
        {
            //When

            var test = typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                  .First(method => method.Name == "LoadDataSources")
                  .Invoke(_bank, null);
            //then
            Assert.IsType<Action<Data.Type>>(test);
        }
        [Fact]

        public void Convert_ReturnsActionType()
        {
            //When

            var test = typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                  .First(method => method.Name == "Convert")
                  .Invoke(_bank, null);
            //then
            Assert.IsType<Action<Data.Type>>(test);
        }
        [Fact]
        public void Cycle_LoadDataSources_InitializesUsersObjectFromFiles()
        {
            //Given
            var test = typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(method => method.Name == "LoadDataSources")
                .Invoke(_bank, null);
            //When
            typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                 .First(method => method.Name == "Cycle")
                 .Invoke(_bank, new[] { test });

            var data = typeof(Bank).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                                    .First(prop => prop.Name == "_users").GetValue(_bank);

            //then
            Assert.IsType<List<User>>(data);
        }
        [Fact]
        public void Cycle_LoadDataSources_InitializesOrganizationsObjectFromFiles()
        {
            //Given
            var test = typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(method => method.Name == "LoadDataSources")
                .Invoke(_bank, null);
            //When
            typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                 .First(method => method.Name == "Cycle")
                 .Invoke(_bank, new[] { test });

            var data = typeof(Bank).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                                    .First(prop => prop.Name == "_organizations").GetValue(_bank);

            //then
            Assert.IsType<List<Organization>>(data);
        }
        [Fact]
        public void CycleLoadDataSources_InitializesTicketsObjectFromFiles()
        {
            //Given
            var test = typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(method => method.Name == "LoadDataSources")
                .Invoke(_bank, null);
            //When
            typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                 .First(method => method.Name == "Cycle")
                 .Invoke(_bank, new[] { test });

            var data = typeof(Bank).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                                    .First(prop => prop.Name == "_tickets").GetValue(_bank);

            //then
            Assert.IsType<List<Ticket>>(data);
        }
        [Fact]
        public void CycleConvert_InitializesTablesFromObjects()
        {
            //Given
            var test = typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
              .First(method => method.Name == "LoadDataSources")
              .Invoke(_bank, null);
            //When
            typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                 .First(method => method.Name == "Cycle")
                 .Invoke(_bank, new[] { test });
            test = typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(method => method.Name == "Convert")
                .Invoke(_bank, null);
            //When
            typeof(Bank).GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                 .First(method => method.Name == "Cycle")
                 .Invoke(_bank, new[] { test });



            //then
            Assert.NotNull(_bank.Data.Tables["Users"]);
            Assert.NotNull(_bank.Data.Tables["Tickets"]);
            Assert.NotNull(_bank.Data.Tables["Organizations"]);
            Assert.Equal(3, _bank.Data.Tables.Count);
        }


    }
}