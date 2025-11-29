using PlainFile.core;
using PlainFiles.Core;

var textFile = new SimpleTextFile("C:\\VISUAL_ESTUDIO\\data.txt");
var lines = textFile.ReadAllLines().ToList();
var opc = string.Empty;

using var logger = new LogWriter(".\\app.log");
logger.WriteLog("INFO", "Aplicacion iniciada");
do
{
    opc = Menu();
    switch (opc)
    {
        case "1":
            Console.WriteLine("contenido del archivo:");
            logger.WriteLog("INFO", "Se mostro el archivo");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
            break;

        case "2":
            Console.Write("ingrese el texto a adicionar:");
            var newLine = Console.ReadLine();
            if (!string.IsNullOrEmpty(newLine))
            {
                lines.Add(newLine);
                Console.WriteLine("linea agregada");
                logger.WriteLog("INFO", $"se agrego: {newLine}");
            }
            else
            {
                Console.WriteLine("No se agrego ninguna linea");
                logger.WriteLog("WARNING", $"No se agrego nada");
            }
            break;

        case "3":
            textFile.WriteAllLines(lines.ToArray());
            Console.WriteLine("archivo guardado");
            logger.WriteLog("INFO", $"se guardo el archivo");
            break;

        case "0":
            Console.WriteLine("saliendo...");
            logger.WriteLog("INFO", $"se salio de la aplicacion");
            break;

        default:
            Console.WriteLine("opcion no valida");
            logger.WriteLog("ERROR", $"selecciono una opcion no valida.");
            break;

    }
} while (opc != "0");
textFile.WriteAllLines(lines.ToArray());
Console.WriteLine("archivo guardado al salir");
logger.WriteLog("INFO", $"se agrego el archivo");



string Menu()
{
	Console.WriteLine("1.   mostrar");
    Console.WriteLine("2.   adicionar");
    Console.WriteLine("3.   guardar");
    Console.WriteLine("0.   salir");
    Console.Write("su opcion es:");
    return Console.ReadLine() ?? string.Empty;
}

