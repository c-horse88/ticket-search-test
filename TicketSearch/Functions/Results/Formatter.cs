

using System;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Text.Json.Serialization;

namespace TicketSearch.Functions.Results
{
    #region Example Output
    // takes results list and itterates through the types and their values outputting child object and values indented for each level down the tree
    // User:
    // [
    //      Organization:
    //      [
    //           Organization:
    //           [
    //                Id: 119
    //                Url: http://initech.zendesk.com/api/v2/organizations/119.json
    //                ExternalId: 2386db7c-5056-49c9-8dc4-46775e464cb7
    //                Name: Multron
    //                DomainNames:
    //                [
    //                     bleeko.com
    //                     pulze.com
    //                     xoggle.com
    //                     sultraxin.com
    //                ]
    //                CreatedAt: 3/1/2016 1:45:12 AM
    //                Details: Non profit
    //                SharedTickets: False
    //                Tags:
    //                [
    //                     Erickson
    //                     Mccoy
    //                     Wiggins
    //                     Brooks
    //                ]
    //           ]
    //      ]
    //      AssignedTickets:
    //      [
    //           Ticket:
    //           [
    //                Id: 1fafaa2a-a1e9-4158-aeb4-f17e64615300
    //                Url: http://initech.zendesk.com/api/v2/tickets/1fafaa2a-a1e9-4158-aeb4-f17e64615300.json
    //                ExternalId: f6f639a4-a8af-4910-804f-5c3a80252653
    //                CreatedAt: 1/16/2016 9:52:49 AM
    //                Type: problem
    //                Subject: A Problem in Russian Federation
    //                Description: Elit exercitation veniam commodo nulla laboris. Dolore occaecat cillum nisi amet in.
    //                Priority: low
    //                Status: solved
    //                SubmitterId: 44
    //                AssigneeId: 1
    //                OrganizationId: 115
    //                Tags:
    //                [
    //                     Georgia
    //                     Tennessee
    //                     Mississippi
    //                     Marshall Islands
    //                ]
    //                HasIncidents: True
    //                DueAt: 8/8/2016 12:10:34 AM
    //                Via: voice
    //           ]
    //           Ticket:
    //           [
    //                Id: 13aafde0-81db-47fd-b1a2-94b0015803df
    //                Url: http://initech.zendesk.com/api/v2/tickets/13aafde0-81db-47fd-b1a2-94b0015803df.json
    //                ExternalId: 6161e938-50cc-4545-acff-a4f23649b7c3
    //                CreatedAt: 3/31/2016 6:35:27 AM
    //                Type: task
    //                Subject: A Problem in Malawi
    //                Description: Lorem ipsum eiusmod pariatur enim. Qui aliquip voluptate cupidatat eiusmod aute velit non aute ullamco.
    //                Priority: urgent
    //                Status: solved
    //                SubmitterId: 42
    //                AssigneeId: 1
    //                OrganizationId: 122
    //                Tags:
    //                [
    //                     New Mexico
    //                     Nebraska
    //                     Connecticut
    //                     Arkansas
    //                ]
    //                HasIncidents: False
    //                DueAt: 8/8/2016 11:25:53 PM
    //                Via: voice
    //           ]
    //      ]
    //      SubmittedTickets:
    //      [
    //           Ticket:
    //           [
    //                Id: fc5a8a70-3814-4b17-a6e9-583936fca909
    //                Url: http://initech.zendesk.com/api/v2/tickets/fc5a8a70-3814-4b17-a6e9-583936fca909.json
    //                ExternalId: e8cab26b-f3b9-4016-875c-b0d9a258761b
    //                CreatedAt: 7/9/2016 3:57:15 AM
    //                Type: problem
    //                Subject: A Nuisance in Kiribati
    //                Description: Ipsum reprehenderit non ea officia labore aute. Qui sit aliquip ipsum nostrud anim qui pariatur ut anim aliqua non aliqua.
    //                Priority: high
    //                Status: open
    //                SubmitterId: 1
    //                AssigneeId: 19
    //                OrganizationId: 120
    //                Tags:
    //                [
    //                     Minnesota
    //                     New Jersey
    //                     Texas
    //                     Nevada
    //                ]
    //                HasIncidents: True
    //                DueAt:  
    //                Via: voice
    //           ]
    //           Ticket:
    //           [
    //                Id: cb304286-7064-4509-813e-edc36d57623d
    //                Url: http://initech.zendesk.com/api/v2/tickets/cb304286-7064-4509-813e-edc36d57623d.json
    //                ExternalId: df00b850-ca27-4d9a-a91a-d5b8d130a79f
    //                CreatedAt: 3/31/2016 9:43:24 AM
    //                Type: task
    //                Subject: A Nuisance in Saint Lucia
    //                Description: Nostrud veniam eiusmod reprehenderit adipisicing proident aliquip. Deserunt irure deserunt ea nulla cillum ad.
    //                Priority: urgent
    //                Status: pending
    //                SubmitterId: 1
    //                AssigneeId: 11
    //                OrganizationId: 106
    //                Tags:
    //                [
    //                     Missouri
    //                     Alabama
    //                     Virginia
    //                     Virgin Islands
    //                ]
    //                HasIncidents: False
    //                DueAt: 8/4/2016 12:44:08 AM
    //                Via: chat
    //           ]
    //      ]
    //      Id: 1
    //      Url: http://initech.zendesk.com/api/v2/users/1.json
    //      ExternalId: 74341f74-9c79-49d5-9611-87ef9b6eb75f
    //      Name: Francisca Rasmussen
    //      Alias: Miss Coffey
    //      CreatedAt: 4/16/2016 1:19:46 AM
    //      Active: True
    //      Verified: True
    //      Shared: False
    //      Locale: en-AU
    //      Timezone: Sri Lanka
    //      LastLoginAt: 8/4/2013 9:03:27 PM
    //      Email: coffeyrasmussen@flotonic.com
    //      Phone: 8335-422-718
    //      Signature: Don't Worry Be Happy!
    //      Tags:
    //      [
    //           Springville
    //           Sutton
    //           Hartsville/Hartley
    //           Diaperville
    //      ]
    //      Suspended: True
    //      Role: admin
    // ]
    #endregion
    public static class Formatter
    {
        private static int _objectLevel;
        private static int _indentSize;

        public static void Format(object element, int indentSize = 2)
        {
            _indentSize = indentSize;
            Build(element);
        }
        private static void Build(object element)
        {
            //If element is a valuetype, null or a string format and write 
            if (element == null || element is ValueType || element is string)
            {
                Write(FormatValue(element));
            }
            else
            {
                var objectType = element.GetType();

                //if not an enumerable write out elements base type and increase level

                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    var baseType = objectType?.BaseType;
                    var typeName = baseType.Name != "Object" ? baseType.Name : objectType.Name;
                    Write("{0}:", typeName);
                    Write("{0}", "[");
                    _objectLevel++;
                }
                //casts element to enumerable and itterates through collection stepping down for subsequent enumerations calling Build where required
                var enumerableElement = element as IEnumerable;
                if (enumerableElement != null)
                {
                    foreach (object item in enumerableElement)
                    {
                        if (item is IEnumerable && !(item is string))
                        {
                            Write("{0}", "[");
                            _objectLevel++;
                            Build(item);
                            _objectLevel--;
                            Write("{0}", "]");
                        }
                        else
                        {
                            Build(item);
                        }
                    }
                }
                //gets member information from non enumerable object looking field and property values
                else
                {
                    MemberInfo[] members = element.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var memberInfo in members)
                    {
                        var fieldInfo = memberInfo as FieldInfo;
                        var propertyInfo = memberInfo as PropertyInfo;

                        if (fieldInfo == null && propertyInfo == null)
                            continue;

                        var type = fieldInfo != null ? fieldInfo.FieldType : propertyInfo.PropertyType;
                        object value = fieldInfo != null
                                        ? fieldInfo.GetValue(element)
                                        : propertyInfo.GetValue(element, null);
                        //check if field or property value is value type and format and write 
                        //Json Ingnore Attribute used to exclude particular fields from output ie id of linked object
                        if (type.IsValueType || type == typeof(string))
                        {
                            if (memberInfo.CustomAttributes.Any(attribute => attribute.AttributeType == typeof(JsonIgnoreAttribute))) continue;
                            Write("{0}: {1}", memberInfo.Name, FormatValue(value));
                        }
                        //continue through enumeration
                        else
                        {
                            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
                            Write("{0}:", memberInfo.Name);
                            Write("{0}", "[");
                            _objectLevel++;
                            Build(value);
                            _objectLevel--;
                            Write("{0}", "]");
                        }
                    }
                }
                //use not being enumerable as the control to step back up the tree and close the object
                if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                {
                    _objectLevel--;
                    Write("{0}", "]");
                }
            }

        }
        //takes string and formats according to objects level and nominated indentsize
        private static void Write(string value, params object[] args)
        {
            var indent = new string(' ', _objectLevel * _indentSize);
            value = (args != null ? string.Format(value, args) : value);
            Console.WriteLine(indent + value);
        }

        //provides formats for different key object types
        private static string FormatValue(object o)
        {
            if (o == null) return (" ");
            if (o is DateTime) return ((o).ToString());
            if (o is string) return string.Format("{0}", o);
            if (o is char && (char)o == '\0') return string.Empty;
            if (o is ValueType) return (o.ToString());
            return ("");
        }
    }
}
