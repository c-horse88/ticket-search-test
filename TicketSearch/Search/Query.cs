using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using TicketSearch.Data;
using TicketSearch.Model;

namespace TicketSearch.Search
{
    public class Query
    {
        public int DataTypeId { get; set; }
        public string DataTypeName { get => DataTypeId != 0 ? _types.First(type => type.Selector == DataTypeId.ToString()).Name : "Unknown"; }
        public string Field { get; set; }
        public dynamic Term { get; set; }
        public System.Type RequiredType { get; set; }
        private Bank _set { get; set; }
        private List<Data.Type> _types { get; set; }
        public Query(List<Data.Type> types, string path = null)
        {
            _types = types;
            _set = new Bank(_types, path);
            _set.Fill();
        }
        public dynamic Execute() { return Build(); }
        private dynamic Build()
        {
            DataTable users = _set.Data.Tables["Users"];
            DataTable tickets = _set.Data.Tables["Tickets"];
            DataTable organizations = _set.Data.Tables["Organizations"];


            switch (_types.First(type => type.Selector == DataTypeId.ToString()).Name)
            {
                case "Users":
                    {
                        return (from user in users.AsEnumerable()
                                join organization in organizations.AsEnumerable()
                                on user.Field<int>("OrganizationId")
                                equals organization.Field<int>("Id")
                                join submittedTicket in tickets.AsEnumerable() on user.Field<int>("Id") equals submittedTicket.Field<int>("SubmitterId") into TicketsSubmitted
                                join assignedTicket in tickets.AsEnumerable() on user.Field<int>("Id") equals assignedTicket.Field<int>("AssigneeId") into TicketsAssigned
                                where RequiredType == typeof(Nullable<DateTime>) ?
                                Term != null ?
                                user.Field<dynamic>(Field).Date == Term.Date :
                                user.Field<dynamic>(Field) == Term :
                                RequiredType == typeof(System.Collections.Generic.List<System.String>) ?
                                Term != "" ?
                                user.Field<dynamic>(Field).Contains(Term) :
                                user.Field<List<string>>(Field).Any() == false :
                                user.Field<dynamic>(Field) == Term
                                select new JoinedUser
                                {
                                    Organization = new Organization().Map(organization),
                                    SubmittedTickets = TicketsSubmitted.Select(ticket => new Ticket().Map(ticket)).ToList(),
                                    AssignedTickets = TicketsAssigned.Select(ticket => new Ticket().Map(ticket)).ToList()
                                }.Map(user)).ToList();
                    }
                case "Tickets":
                    {
                        return (from ticket in tickets.AsEnumerable()
                                join organization in organizations.AsEnumerable() on ticket.Field<int>("OrganizationId") equals organization.Field<int>("Id")
                                join submitted in users.AsEnumerable() on ticket.Field<int>("SubmitterId") equals submitted.Field<int>("Id")
                                join assigned in users.AsEnumerable() on ticket.Field<int>("AssigneeId") equals assigned.Field<int>("Id")
                                where RequiredType == typeof(Nullable<DateTime>) ?
                                Term != null ?
                                ticket.Field<dynamic>(Field).Date == Term.Date :
                                ticket.Field<dynamic>(Field) == Term :
                                RequiredType == typeof(System.Collections.Generic.List<System.String>) ?
                                Term != "" ?
                                ticket.Field<dynamic>(Field).Contains(Term) :
                                ticket.Field<List<string>>(Field).Any() == false :
                                ticket.Field<dynamic>(Field) == Term
                                select new JoinedTicket
                                {
                                    Organization = new Organization().Map(organization),
                                    Submitter = new User().Map(submitted),
                                    Assignee = new User().Map(assigned)
                                }.Map(ticket)).ToList();
                    }
                case "Organizations":
                    {
                        return (from organization in organizations.AsEnumerable()
                                join user in users.AsEnumerable()
                                on organization.Field<int>("Id")
                                equals user.Field<int>("OrganizationId") into Users
                                join ticket in tickets.AsEnumerable() on organization.Field<int>("Id") equals ticket.Field<int>("OrganizationId") into Tickets
                                where RequiredType == typeof(Nullable<DateTime>) ?
                                Term != null ?
                                organization.Field<dynamic>(Field).Date == Term.Date :
                                organization.Field<dynamic>(Field) == Term :
                                RequiredType == typeof(System.Collections.Generic.List<System.String>) ?
                                Term != "" ?
                                organization.Field<dynamic>(Field).Contains(Term) :
                                organization.Field<List<string>>(Field).Any() == false :
                                organization.Field<dynamic>(Field) == Term
                                select new JoinedOrganisation
                                {
                                    Users = Users.Select(user => new User().Map(user)).ToList(),
                                    Tickets = Tickets.Select(ticket => new Ticket().Map(ticket)).ToList(),
                                }.Map(organization)).ToList();
                    }
                default:
                    throw new Exception("Invalid DataType selected");
            }
        }
    }
}

