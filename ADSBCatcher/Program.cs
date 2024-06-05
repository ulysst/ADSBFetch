using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumWebDriverDemo
{
    abstract class Program
    {
        static void Main(string[] args)
        {
            WebPageInit wp = new WebPageInit();
            wp.OpenBrowser("https://globe.adsbexchange.com/");
            Thread.Sleep(1000);
            /*-----------------------------Setup--------------------------------*/
            // Remove the cookies window
            wp.ClickButtonByXPathWithIframe("//*[@id=\"notice\"]/div[3]/div[2]/button", "sp_message_iframe_1116549");
            
            // Only show military units
            wp.ClickButtonByXPath("//*[@id=\"U\"]");
            
            // Puts in the list every plane from view
            wp.ClickButtonByXPath("//*[@id=\"allTableLines_cb\"]");
            
            // Tweaks the filter options to allow only the ones wanted in the board
            Thread.Sleep(1000);
            wp.ManageFilter();  
            
            // Zooms Out MAP
            Thread.Sleep(1000);
            wp.ZoomOut(7);
            
            /*-----------------------CreateJSONServer--------------------------*/
         
            Thread serverThread = new Thread(StartJsonServer);
            serverThread.Start();

            /*---------------------Fetching and Exporting----------------------*/
            while (true) 
            {
                // Fetch data
                FetchBoard fb = new FetchBoard();
                var planes = fb.GetTextFromXPath(wp.GetDriver(),"//*[@id=\"planesTable\"]\n");
                
                // Export data
                var jsonWriter = new JsonExport();
                jsonWriter.WriteToJsonFile(planes, "output.json");
                
                // Sleep for some time before fetching again
                Thread.Sleep(1000);
            }
            
            /*---------------------------Closing-------------------------------*/
            wp.CloseBrowser(100000);
        }

        static void StartJsonServer()
        {
            OpenJSONServer opj = new OpenJSONServer();
            opj.StartJsonServer();
            
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
        }
        
        static void OnProcessExit(object sender, EventArgs e)
        {
            // Close port activity
            OpenJSONServer opj = new OpenJSONServer();
            opj.ClosePortActivity(3001); // Change the port number as needed
        }
    }
}
