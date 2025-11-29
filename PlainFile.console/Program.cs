using PlainFile.core;

var people = new List<string[]>
{
    new [] { "FirstName", "LastName", "Age" },
    new [] { "John", "Doe", "30" },
    new [] { "Jane", "Smith", "25" },
    new [] { "Sam", "Brown", "40" }
};

var manualCsv =new ManualCsvHelper ();
manualCsv.WriteCsv("people.csv", people);

var loaded = manualCsv.ReadCsv("people.csv");
foreach (var person in loaded)
{
    Console.WriteLine(string.Join("|", person));
}