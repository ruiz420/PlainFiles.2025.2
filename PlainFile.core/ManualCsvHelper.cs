

namespace PlainFiles.Core;

public class ManualCsvHelper
{
    public void WriteCsv(string path, List<string[]> records)
    {
        EnsureDirectoryExists(path);

        using var sw = new StreamWriter(path, false); // crea o sobrescribe

        foreach (var record in records)
        {
            var line = string.Join(",", record);
            sw.WriteLine(line);
        }
    }

    public List<string[]> ReadCsv(string path)
    {
        EnsureDirectoryExists(path);
        if (!File.Exists(path))
        {
            using var fs = File.Create(path);
            return new List<string[]>();  // no hay nada que leer, regresamos vacío
        }

        var result = new List<string[]>();

        using var sr = new StreamReader(path);
        string? line;

        while ((line = sr.ReadLine()) != null)
        {
            var fields = line.Split(',');
            result.Add(fields);
        }

        return result;
    }



    private static void EnsureDirectoryExists(string path)
    {
        var directory = Path.GetDirectoryName(path);

        if (string.IsNullOrEmpty(directory))
            directory = Environment.CurrentDirectory;

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }
}
