namespace PlainFile.core;

public class LogWriter : IDisposable
{
    private readonly StreamWriter _writer;

    public LogWriter(string Path)
    {
        _writer = new StreamWriter(Path, append: true)
        {
            AutoFlush = true
        };
    }

    public void WriteLog(string Level, string messaje)
    {
        var timestamp = DateTime.Now.ToString("s");
        _writer.WriteLine($"[{timestamp}] - [{Level}] - {messaje}");
    }

    public void Dispose()
    {
        _writer?.Dispose();
    }
}
