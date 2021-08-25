
using RelevantCodes.ExtentReports;
using MarsFramework.Config;
using MarsFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using static MarsFramework.Global.GlobalDefinitions;

namespace MarsFramework.Global
{
    public class Base
    {
        #region To access Path from resource file

        public static int Browser = Int32.Parse(MarsResource.Browser);
        public static String ExcelPath = MarsResource.ExcelPath;
        public static string ScreenshotPath = MarsResource.ScreenShotPath;
        public static string ReportPath = MarsResource.ReportPath;
        #endregion

        #region reports
        public static ExtentTest test;
        public static ExtentReports extent;
       
        public static void ExtentReports()
        {
            extent = new ExtentReports(MarsResource.ReportPath, false, DisplayOrder.NewestFirst);
            extent.LoadConfig(MarsResource.ReportXMLPath ); 
        }

        #endregion

          

        #region setup and tear down
        [SetUp]
        [Obsolete]
        public void Inititalize()
        {
          
            switch (Browser)
            {

                case 1:
                    GlobalDefinitions.driver = new FirefoxDriver();
                    break;
                case 2:
                    GlobalDefinitions.driver = new ChromeDriver();
                    GlobalDefinitions.driver.Manage().Window.Maximize();
                    break;

            }

            //Launch the portal
            GlobalDefinitions.driver.Navigate().GoToUrl("http://localhost:5000");


            if (MarsResource.IsLogin == "true")
            {
                SignIn loginobj = new SignIn();
                loginobj.LoginSteps();
            }
            else
            {
                SignUp obj = new SignUp();
                obj.register();
            }

        }


        [TearDown]
        public void TearDown()
        {
            // Screenshot
            String img = SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "Report");
            test.Log(LogStatus.Info, "Snapshot below: " + test.AddScreenCapture(img));
            // end test. (Reports)
            extent.EndTest(test);
            // calling Flush writes everything to the log file (Reports)
            extent.Flush();
            // Close the driver :)            
           driver.Close();
            driver.Quit();
        }
        #endregion

    }
}