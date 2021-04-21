using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using TicketSearch.Model;
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("TicketSearch.Test")]
namespace TicketSearch.Data
{
    public class Bank
    {
        private string _path { get; set; } = Path.Combine(Environment.CurrentDirectory, "Files/JSON");
        private List<User> _users { get; set; }
        private List<Ticket> _tickets { get; set; }
        private List<Organization> _organizations { get; set; }
        public DataSet Data { get => _data; }
        private DataSet _data = new DataSet();
        private List<Type> _dataTypes { get; set; }
        public Bank(List<Type> dataTypes, string path = null)
        {
            _dataTypes = dataTypes;
            if (path != null) _path = path;

        }
        public void Fill()
        {

            Cycle(LoadDataSources());
            Cycle(Convert());
        }
        private Action<Type> LoadDataSources()
        {
            return (Type type) =>
             {
                 //prepare full path to data including
                 var path = Path.Combine(_path, $"{type.Name.ToLower()}.json");

                 //Read file and dynamically call the data objects own "Get" method using reflection
                 try
                 {
                     var stringData = File.ReadAllText(path);
                     var data = type.Object.GetType().GetMethod("Get").Invoke(type.Object, new object[] { stringData });

                     //Set the data object as the value to relevant Property using reflection
                     this.GetType().GetProperty($"_{type.Name.ToLower()}", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(this, data);
                 }
                 catch (System.IO.FileNotFoundException)
                 {
                     Console.WriteLine($"Unable to load data for from file Type:{type.Name} Expected Location:{path}\nPress any key to return to the menu");
                     Console.ReadKey();
                     Menu.Main.Execute(Menu.Options.Get());
                 }
                 catch (System.Exception ex)
                 {
                     Console.WriteLine($"Unexpected Error Occured: {ex.Message}.\nPress Any key to exit");
                     Console.ReadKey();
                     Environment.Exit(1);
                     throw;
                 }

             };

        }

        private Action<Type> Convert()
        {
            return (Type type) =>
             {
                 //Find and retrieve data from DataBank's Properties using type
                 var dataPropInfo = this.GetType().GetProperty($"_{type.Name.ToLower()}", BindingFlags.NonPublic | BindingFlags.Instance);
                 dynamic data = dataPropInfo.GetValue(this);

                 //Convert to data to DataTable and add to DataBank's DataSet
                 DataTable table = ConvertToDataTable(data);
                 table.TableName = type.Name;
                 _data.Tables.Add(table);
             };
        }
        private void Cycle(Action<Type> complete)
        {
            foreach (var type in _dataTypes) complete(type);
        }
        private DataTable ConvertToDataTable(IEnumerable<dynamic> data)
        {
            if (data.Count() == 0) throw new Exception("IEnumerable<dynamic> data contains has a count of 0. parameters must provide a IEnumerable with a non zero count");
            var properties = TypeDescriptor.GetProperties(data.First().GetType());
            var table = new DataTable();
            foreach (var prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (dynamic item in data)
            {
                DataRow row = table.NewRow();
                foreach (var prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}