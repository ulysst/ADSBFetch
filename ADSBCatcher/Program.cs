using System;
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
            /*---------------------------Fetching------------------------------*/
            Thread.Sleep(5000);
            FetchBoard fb = new FetchBoard();
            var planes = fb.GetTextFromXPath(wp.GetDriver(),"//*[@id=\"planesTable\"]\n");
            
            /*---------------------------Exporting-----------------------------*/
            
            var jsonWriter = new JsonExport();
            jsonWriter.WriteToJsonFile(planes, "output.json");
            
            /*---------------------------Closing-------------------------------*/
            wp.CloseBrowser(100000);
        }
    }
}