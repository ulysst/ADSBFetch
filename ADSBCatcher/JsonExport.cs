namespace SeleniumWebDriverDemo;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class JsonExport
{
    public void WriteToJsonFile(List<string> planes, string filePath)
    {
        // Convert list of planes to JSON
        string json = JsonSerializer.Serialize(new { plane = planes });

        // Write JSON to file
        File.WriteAllText(filePath, json);
    }
}