namespace SeleniumWebDriverDemo;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

public class JsonExport
{
    public void WriteToJsonFile(List<FlightData> data, string filePath)
    {
        // Create a dictionary to store FlightData objects keyed by callsign
        var planesDictionary = new Dictionary<string, FlightData>();

        // Populate the dictionary with FlightData objects keyed by callsign
        foreach (var flightData in data)
        {
            planesDictionary[flightData.Callsign] = flightData;
        }

        // Create a parent object with a "planes" property containing the planes dictionary
        var jsonData = new { planes = planesDictionary };

        // Serialize the parent object to JSON
        string json = JsonConvert.SerializeObject(jsonData, Formatting.Indented);

        // Write JSON to file
        File.WriteAllText(filePath, json);
    }
}