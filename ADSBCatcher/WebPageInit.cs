// BrowserHelper.cs
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace SeleniumWebDriverDemo
{
    // Define a custom class to hold filter option information
    public class FilterOption
    {
        public string XPath { get; set; }
        public bool IsSelected { get; set; }
    }
    public class WebPageInit
    {
        private IWebDriver driver;
        private string XPathForCollumsButton = "//*[@id='ui-id-2']";

       private List<FilterOption> filterOptions = new List<FilterOption>
        {
            new FilterOption { XPath = "//*[@id='column_icao_cb']", IsSelected = false },           // HEXID
            new FilterOption { XPath = "//*[@id='column_flag_cb']", IsSelected = false },           // FLAG
            new FilterOption { XPath = "//*[@id='column_flight_cb']", IsSelected = true },         // CALLSIGN
            new FilterOption { XPath = "//*[@id='column_registration_cb']", IsSelected = true },   // REGISTRATION
            new FilterOption { XPath = "//*[@id='column_aircraft_type_cb']", IsSelected = false },  // TYPE
            new FilterOption { XPath = "//*[@id='column_squawk_cb']", IsSelected = false },         // SQUAWK
            new FilterOption { XPath = "//*[@id='column_altitude_cb']", IsSelected = true },       // ALTITUDE
            new FilterOption { XPath = "//*[@id='column_speed_cb']", IsSelected = true },          // SPEED
            new FilterOption { XPath = "//*[@id='column_vert_rate_cb']", IsSelected = true },      // VERTICAL RATE
            new FilterOption { XPath = "//*[@id='column_distance_cb']", IsSelected = false },       // DISTANCE
            new FilterOption { XPath = "//*[@id='column_track_cb']", IsSelected = false },          // TRACK
            new FilterOption { XPath = "//*[@id='column_msgs_cb']", IsSelected = false },           // MESSAGE
            new FilterOption { XPath = "//*[@id='column_seen_cb']", IsSelected = false },           // SEEN
            new FilterOption { XPath = "//*[@id='column_rssi_cb']", IsSelected = false },           // RSSI
            new FilterOption { XPath = "//*[@id='column_lat_cb']", IsSelected = true },            // LATITUDE
            new FilterOption { XPath = "//*[@id='column_lon_cb']", IsSelected = true },            // LONGITUDE
            new FilterOption { XPath = "//*[@id='column_data_source_cb']", IsSelected = false },    // SOURCE
            new FilterOption { XPath = "//*[@id='column_military_cb']", IsSelected = false },       // MILITARY
            new FilterOption { XPath = "//*[@id='column_wd_cb']", IsSelected = true },            // WIND DIRECTION
            new FilterOption { XPath = "//*[@id='column_ws_cb']", IsSelected = true }             // WIND SPEED
        };
        public void OpenBrowser(string url)
        {
            try
            {
                var chromeOptions = new ChromeOptions();

                // Initialize the Chrome driver
                driver = new ChromeDriver(chromeOptions);
                
                driver.Manage().Window.Maximize();

                // Navigate to a webpage
                driver.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void CloseBrowser(int delayInSeconds)
        {
            try
            {
                // Wait for a specified time before closing the browser
                System.Threading.Thread.Sleep(delayInSeconds * 1000);

                // Close the browser
                driver.Quit();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while closing the browser: " + ex.Message);
            }
        }
        public void ClickButtonByXPathWithIframe(string XPath, string iframeId)
        {
            try
            {
                // Switch to the iframe
                driver.SwitchTo().Frame(iframeId);

                // Locate the button directly
                IWebElement button = driver.FindElement(By.XPath(XPath));

                // Click the button
                button.Click();
                Console.WriteLine("Button clicked successfully.");

                // Switch back to the default content
                driver.SwitchTo().DefaultContent();
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Button not found: " + ex.Message);
            }
            catch (ElementNotInteractableException ex)
            {
                Console.WriteLine("Button not interactable: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while clicking the button: " + ex.Message);
            }
        }

        public void ClickButtonByXPath(string XPath)
        {
            try
            {
                // Locate the button directly
                IWebElement button = driver.FindElement(By.XPath(XPath));

                // Click the button
                button.Click();
                Console.WriteLine("Button clicked successfully.");
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Button not found: " + ex.Message);
            }
            catch (ElementNotInteractableException ex)
            {
                Console.WriteLine("Button not interactable: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while clicking the button: " + ex.Message);
            }
        }

        public void ResetFilters()
        {
            try
            {
                ClickButtonByXPath("//*[@id=\"tabs\"]/ul/li[3]");
                Thread.Sleep(1000);
                // Define the XPaths for the filters to reset
                string[] filterXPaths = {
                    "//*[@id='column_icao_cb']",          // HEXID
                    "//*[@id='column_flag_cb']",          // FLAG
                    "//*[@id='column_flight_cb']",        // CALLSIGN
                    "//*[@id='column_aircraft_type_cb']", // TYPE
                    "//*[@id='column_altitude_cb']",      // ALTITUDE
                    "//*[@id='column_speed_cb']",         // SPEED
                    "//*[@id='column_distance_cb']",      // DISTANCE
                    "//*[@id='column_wd_cb']",           // WIND DIRECTION
                    "//*[@id='column_ws_cb']"            // WIND SPEED
                };

                // Click the buttons for each filter
                foreach (string xpath in filterXPaths)
                {
                    ClickButtonByXPath(xpath);
                }

                Console.WriteLine("Filters reset successfully.");
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Filter button not found: " + ex.Message);
            }
            catch (ElementNotInteractableException ex)
            {
                Console.WriteLine("Filter button not interactable: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while resetting filters: " + ex.Message);
            }
            
        }
        
        public void ManageFilter()
        {
            
            ResetFilters();
            Thread.Sleep(1000);
            try
            {
                foreach (var option in filterOptions)
                {
                    try
                    {
                        // If the option is selected, click it
                        if (option.IsSelected)
                        {
                            ClickButtonByXPath(option.XPath);
                        }
                        else
                        {
                            // Option is not selected, skip clicking
                            Console.WriteLine($"Filter option '{option.XPath}' is not selected.");
                        }
                    }
                    catch (NoSuchElementException ex)
                    {
                        Console.WriteLine($"Filter option '{option.XPath}' not found: {ex.Message}");
                    }
                    catch (ElementNotInteractableException ex)
                    {
                        Console.WriteLine($"Filter option '{option.XPath}' not interactable: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            $"An error occurred while managing filter option '{option.XPath}': {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while managing filter options: " + ex.Message);
            }
        }

        public void ZoomOut(int times)
        {
            for (int i = 0; i < times; i++)
            {
                ClickButtonByXPath("//*[@id=\"map_canvas\"]/div[3]/button[2]");
            }
        }
        
        public List<string> GetTextFromXPath(string XPath)
        {
            List<string> textList = new List<string>();

            try
            {
                // Locate the element using the provided XPath
                IWebElement element = driver.FindElement(By.XPath(XPath));

                // Get the visible text of the element
                string text = element.Text;

                // Add the text to the list
                textList.Add(text);

                Console.WriteLine("Text retrieved successfully: " + text);
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine("Element not found: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving text: " + ex.Message);
            }

            return textList;
        }

        public IWebDriver GetDriver()
        {
            return driver;
        }
    }
}