using MarsFramework.Global;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Threading;
using static MarsFramework.Global.GlobalDefinitions;
using static MarsFramework.Global.Base;
using RelevantCodes.ExtentReports;

namespace MarsFramework.Pages
{
    public class ManageListings : ShareSkill
    {
        [Obsolete]
        public ManageListings()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        //Click on Manage Listings Link
        [FindsBy(How = How.XPath, Using = "//*[@id='account-profile-section']/div/section[1]/div/a[3]")]
        public IWebElement manageListingsLink { get; set; }

        //View the listing
        [FindsBy(How = How.XPath, Using = "(//i[@class='eye icon'])[1]")]
        public IWebElement view { get; set; }

        //Delete the listing
        [FindsBy(How = How.XPath, Using = "//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[8]/div/button[3]/i")]
        public IWebElement delete { get; set; }

        //Edit the listing
        [FindsBy(How = How.XPath, Using = "(//i[@class='outline write icon'])[1]")]
        public IWebElement edit { get; set; }

        //Click on Yes or No
        [FindsBy(How = How.XPath, Using = "//div[@class='actions']")]
        public IWebElement clickActionsButton { get; set; }

        //Click on Yes  
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div[3]/button[2]")]
        public IWebElement clickYesButton { get; set; }

        //Click on No 
        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div[3]/button[1]")]
        public IWebElement clickNoButton { get; set; }

        //Select category for the top row
        [FindsBy(How = How.XPath, Using = "//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[2]")]
        public IWebElement categoryType { get; set; }
       

        //Select Title for the top row
        [FindsBy(How = How.XPath, Using = "//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[3]")]
        public IWebElement titleText { get; set; }

        //Select Description for the top row
        [FindsBy(How = How.XPath, Using = "//*[@id='listing-management-section']/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[4]")]
        public IWebElement descriptionText { get; set; }

       //Delete confirmation  Message
        [FindsBy(How = How.XPath, Using = "//*[@id='listing-management-section']/div[2]/h3")]
        public IWebElement messageDisplay { get; set; }


        #region Edit service listings
        public void EditServiceListing()
        {
            //Click on ManageListing Link
            manageListingsLink.WaitForElementClickable(Global.GlobalDefinitions.driver, 60);
            manageListingsLink.Click();

            //Click on edit service Icon
            edit.WaitForElementClickable(Global.GlobalDefinitions.driver, 60);
            edit.Click();

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "ManageListings");

            Thread.Sleep(2000);
            //Edit the title and read from Excel
            title.Clear();
            GlobalDefinitions.wait(1);
            title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Title"));

            //Enter Description
            description.Clear();
            GlobalDefinitions.wait(1);
            description.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

            //Click on Save listing button
            Save.WaitForElementClickable(Global.GlobalDefinitions.driver, 60);          
                Save.Click();
        }
        #endregion

        #region Validate edit service listing functionality
        public void ValidateEditServiceListing()
        {
            GlobalDefinitions.wait(3);
            string expectedTitle = "Software Testing";
            string expectedDescription = "Experience in both Manual and Automatic Testing";

            try
            {
                //Start the Reports
                Base.ExtentReports();
                Thread.Sleep(1000);
                Base.test = Base.extent.StartTest("Edit a Service Listing");
                test.Log(LogStatus.Info, "Editing a listing");
                titleText.WaitForElementClickable(GlobalDefinitions.driver, 60);

                //Verify if expected value and actual values are same for Title and description content
                Assert.IsTrue((titleText.Text == expectedTitle && descriptionText.Text == expectedDescription));
                Thread.Sleep(2000);
                test.Log(LogStatus.Pass, "Test Passed, Service listing edited Successfully");
                SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "SkillsEditedSuccessfully");
                Assert.IsTrue(true);
            }

            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "Test Failed");
                Console.WriteLine(ex.Message);
            }

        }
        #endregion


        #region Delete Service Listing
        public void DeleteListings()
        {
            //wait for element to be clickable
            manageListingsLink.WaitForElementClickable(Global.GlobalDefinitions.driver, 60);
            //click on Manage listing Link
            manageListingsLink.Click();

            //click on delete link
            delete.WaitForElementClickable(Global.GlobalDefinitions.driver, 60);
            delete.Click();

            //Wait for Click Yes button element
            clickYesButton.WaitForElementClickable(Global.GlobalDefinitions.driver, 60);
            //Confirm Delete operation
            clickYesButton.Click();
            
        }
        #endregion


        #region Validate delete listings functionality
        public void ValidateDeleteListing()
        {
            GlobalDefinitions.wait(5);
            //wait for TitleText to be clickable
            string actualMessageDisplay = "You do not have any service listings!";
            string expectedMessageDisplay = messageDisplay.Text;
           
            try
            {
                //Start the Reports
                Base.ExtentReports();
                Thread.Sleep(1000);
                Base.test = Base.extent.StartTest("Delete a Service Listing");
                test.Log(LogStatus.Info, "Delete a listing");

                //verify if skill listing is deleted deleted 
                Assert.AreEqual(actualMessageDisplay, expectedMessageDisplay);

                test.Log(LogStatus.Pass, "Test Passed, Service listing deleted Successfully");
                SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "SkillsdeleteSuccessful");
                Assert.IsTrue(true);

            }

            catch (Exception ex)
            {
                test.Log(LogStatus.Fail, "Test Failed");
                Console.WriteLine(ex.Message);
            }

        }
        #endregion

    }
}

