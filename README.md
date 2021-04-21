# Ticket Search Test

Sample application demonstrating my approach to providing the end user with a simple to interact with search utility using a set of JSON files as the data source.

## Prerequisites

- [.Net Core 3.1]("https://dotnet.microsoft.com/download/dotnet/3.1")

## Build

```bash
dotnet build
```

## Test

```bash
dotnet test
```

## Run

```bash
dotnet run
```

## Assumptions/Tradeoffs

- A single level of joined data is an acceptable depth. ie Searching by User joins only the directly linked organization tickets but not child users.
- Searching for a date only requires a date due as it is unlikely a user would enter a date and a time
- No ability to search for a blank field against Int type fields.
- Tests are only centred around the TypeTranslator and Data Bank classes. Much of the user input validation logic is internalised as a private function that is reliant on the execute method.
- using JSONIgnoreAttribute in the object to exclude fields from output this serves another purpose if a json file output function was used it would also exclude them. This is used in the objects that represent the joined data.
- Searching for organisations relies on the timezone when looking at created by.

## Reflection

This is one of many ways to approach building an application that can achieve the function of searching a dataset. Adding additional datatypes should be straightforward as the list of data types and menu options can easily be expanded upon to open up new data types and functions. Updating the data types would require changes to be made to the query class to ensure relevant data is presented.

Separation of concerns could have been achieved in a more complete fashion regarding functionality from step to step and also within the steps themselves. Search for example is basically stand alone but relies on calling the Main.Menu.Execute function.
