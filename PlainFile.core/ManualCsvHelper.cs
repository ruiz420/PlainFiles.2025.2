// CSV COMMA SEPARATED VALUES
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainFile.core;

public class ManualCsvHelper
{
    public void WriteCsv(string path, List<string[]> records)
    {
        using var sw = new StreamWriter(path);
        foreach (var record in records) ;
        {
            var line = string.Join(",", records);
            sw.WriteLine(line);
        }

    }

    public List<string[]> ReadCsv(string path)
    {
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
}

