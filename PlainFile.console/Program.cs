using PlainFiles.Core;

Console.Write("Digite el nombre de la lista: ");
var listName = Console.ReadLine();

// Instancia de tu manejador CSV
var manualCsv = new ManualCsvHelper();

// 🟢 Siempre intenta cargar el archivo (si no existe, lo crea vacío)
var filePath = $"{listName}.csv";
var people = manualCsv.ReadCsv(filePath);

string option;
do
{
    option = MyMenu();

    switch (option)
    {
        case "1":
            Console.WriteLine("Digite el nombre: ");
            var name = Console.ReadLine();

            Console.WriteLine("Digite el apellido: ");
            var lastName = Console.ReadLine();

            Console.WriteLine("Digite la edad: ");
            var age = Console.ReadLine();

            people.Add(new string[]
            {
                name ?? "",
                lastName ?? "",
                age ?? ""
            });
            break;

        case "2":
            Console.WriteLine("Lista de personas:");
            Console.WriteLine("Nombre | Apellido | Edad");

            foreach (var person in people)
            {
                    Console.WriteLine($"{person[0]} | {person[1]} | {person[2]}");
            }
            break;

        case "3":
            SaveFile(people, filePath, manualCsv);
            Console.WriteLine("Archivo guardado.");
            break;

        case "0":
            Console.WriteLine("Saliendo...");
            break;

        default:
            Console.WriteLine("Opción no válida.");
            break;
    }

} while (option != "0");

// Guarda automáticamente al salir
SaveFile(people, filePath, manualCsv);


// --- MÉTODOS LOCALES ----

string MyMenu()
{
    Console.WriteLine("1. Adicionar");
    Console.WriteLine("2. Mostrar");
    Console.WriteLine("4. Eliminar.");
    Console.WriteLine("5. Ordenar.");
    Console.WriteLine("3. Guardar");
    Console.WriteLine("0. Salir");
    Console.Write("Seleccione una opción: ");
    return Console.ReadLine() ?? "";
}

void SaveFile(List<string[]> data, string path, ManualCsvHelper csv)
{
    csv.WriteCsv(path, data);
}
