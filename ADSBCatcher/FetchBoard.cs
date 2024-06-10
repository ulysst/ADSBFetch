using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SeleniumWebDriverDemo
{
    public class FetchBoard
    {
        public List<FlightData> GetTextFromXPath(IWebDriver driver, string XPath)
        {
            List<FlightData> flightDataList = new List<FlightData>();

            try
            {
                // Locate the element using the provided XPath
                IWebElement element = driver.FindElement(By.XPath(XPath));

                // Get the visible text of the element
                string text = element.Text;

                // Split the text into lines
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                // Iterate through each line
                foreach (string line in lines)
                {
                    // Split the line by spaces
                    string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    // Check if there are exactly 6 parts
                    if (parts.Length == 7)
                    {
                        // Create a FlightData object
                        FlightData flightData = new FlightData
                        {
                            Callsign = parts[0],
                            Registration = parts[1],
                            Altitude = parts[2],
                            Speed = parts[3],
                            VerticalRate = parts[4],
                            Latitude = parts[5],
                            Longitude = parts[6],
                        };

                        // Add FlightData object to the list
                        flightDataList.Add(flightData);
                    }
                }

                Console.WriteLine("Text retrieved successfully.");
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Element not found: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving text: " + ex.Message);
            }

            return flightDataList;
        }
    }

    public class FlightData
    {
        public string Callsign { get; set; }
        public string Registration { get; set; }
        public string Altitude { get; set; }
        public string Speed { get; set; }
        public string VerticalRate { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
