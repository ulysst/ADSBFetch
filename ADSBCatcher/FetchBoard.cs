using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SeleniumWebDriverDemo
{
    public class FetchBoard
    {
        public List<string> GetTextFromXPath(IWebDriver driver, string XPath)
        {
            List<string> planes = new List<string>();

            try
            {
                // Locate the element using the provided XPath
                IWebElement element = driver.FindElement(By.XPath(XPath));

                // Get the visible text of the element
                string text = element.Text;

                // Split the text into lines
                string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                // Check each line for six spaces and add to the list if found
                foreach (string line in lines)
                {
                    if (LineHasSixSpaces(line))
                    {
                        string cleanedLine = CleanLine(line);
                        planes.Add(cleanedLine);
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

            return planes;
        }

        private bool LineHasSixSpaces(string line)
        {
            // Split the line by spaces
            string[] parts = line.Split(' ');

            // Count the number of spaces
            int spaceCount = parts.Length - 1; // Subtract 1 since Split always returns one more element than the number of separators

            // Return true if the number of spaces is exactly 6
            return spaceCount == 6;
        }

        private string CleanLine(string line)
        {
            // Remove any character that is not a letter, number, or space
            // Remove narrow no-break spaces (Unicode: U+202F)
            string cleanedLine = new string(line.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray());

            // Remove specific unwanted characters
            cleanedLine = Regex.Replace(cleanedLine, @"[\u202F]", string.Empty);

            return cleanedLine;
        }
    }
}
