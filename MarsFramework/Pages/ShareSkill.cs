using AutoIt;
using MarsFramework.Global;
using Microsoft.JScript;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using static MarsFramework.Global.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static MarsFramework.Global.GlobalDefinitions;
using RelevantCodes.ExtentReports;

namespace MarsFramework.Pages
{
   public class ShareSkill 
    {
        [Obsolete]
        public ShareSkill()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        //Click on ShareSkill Button
        [FindsBy(How = How.LinkText, Using = "Share Skill")]
        public IWebElement ShareSkillButton { get; set; }

        //Enter the Title in textbox
        [FindsBy(How = How.Name, Using = "title")]
        public IWebElement title { get; set; }

        //Enter the Description in textbox
        [FindsBy(How = How.Name, Using = "description")]
        public IWebElement description { get; set; }

        //Click on Category Dropdown
        [FindsBy(How = How.Name, Using = "categoryId")]
        public IWebElement CategoryDropDown { get; set; }

        //Click on SubCategory Dropdown
        [FindsBy(How = How.Name, Using = "subcategoryId")]
        public IWebElement SubCategoryDropDown { get; set; }

        //Enter Tag names in textbox
        [FindsBy(How = How.XPath, Using = "//body/div/div/div[@id='service-listing-section']/div[contains(@class,'ui container')]/div[contains(@class,'listing')]/form[contains(@class,'ui form')]/div[contains(@class,'tooltip-target ui grid')]/div[contains(@class,'twelve wide column')]/div[contains(@class,'')]/div[contains(@class,'ReactTags__tags')]/div[contains(@class,'ReactTags__selected')]/div[contains(@class,'ReactTags__tagInput')]/input[1]")]
        public IWebElement Tags { get; set; }

        //Select the Service type
        [FindsBy(How = How.XPath, Using = "//form/div[5]/div[@class='twelve wide column']/div/div[@class='field']")]
        public IWebElement ServiceTypeOptions { get; set; }

        //Select the Location Type
        [FindsBy(How = How.XPath, Using = "//form/div[6]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        public IWebElement LocationTypeOption { get; set; }

        //Click on Start Date dropdown
        [FindsBy(How = How.Name, Using = "startDate")]
        public IWebElement StartDateDropDown { get; set; }

        //Click on End Date dropdown
        [FindsBy(How = How.Name, Using = "endDate")]
        public IWebElement EndDateDropDown { get; set; }

        //Click on Skill Trade option
        [FindsBy(How = How.XPath, Using = "//form/div[8]/div[@class='twelve wide column']/div/div[@class = 'field']")]
       IWebElement SkillTradeOption { get; set; }

        //Enter Skill Exchange
        [FindsBy(How = How.XPath, Using = "//div[@class='form-wrapper']//input[@placeholder='Add new tag']")]
        public IWebElement SkillExchange { get; set; }
       
        //Click on Credit option
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[8]/div[2]/div/div[2]/div/input")]
        public IWebElement Credit { get; set; }

        //Enter the amount for Credit
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Amount']")]
        public IWebElement CreditAmount { get; set; }

        //Click on Active/Hidden option
        [FindsBy(How = How.XPath, Using = "//form/div[10]/div[@class='twelve wide column']/div/div[@class = 'field']")]
        public IWebElement ActiveOption { get; set; }

        //Click on File upload
        [FindsBy(How = How.XPath, Using = "//*[@id='service-listing-section']/div[2]/div/form/div[9]/div/div[2]/section/div/label/div/span/i")]
        public IWebElement FileUpload { get; set; }

        //Click on Save button
        [FindsBy(How = How.XPath, Using = "//input[@value='Save']")]
        public IWebElement Save { get; set; }

      
        # region Add service listing
        public void EnterShareSkill()
        {
            ShareSkillButton.WaitForElementClickable(Global.GlobalDefinitions.driver, 60);

            //Click on share skill button and navigate to Skill listing page
            ShareSkillButton.Click();

            //Populate the excel data
            GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkills");

            Thread.Sleep(3000);
            //Enter Title
            title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Title"));

            //Enter Description
            Thread.Sleep(3000);
            description.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Description"));

            #region select a category and sub category
            //Select category
            Thread.Sleep(3000);
            SelectElement categoryDropDownList = new SelectElement(CategoryDropDown);
            Random rnd = new Random();
            int count = rnd.Next(1, 8);
            categoryDropDownList.SelectByIndex(count);

            //Select subcategory
            Thread.Sleep(1000);
            SelectElement subCategoryDropDownList = new SelectElement(SubCategoryDropDown);
            subCategoryDropDownList.SelectByIndex(rnd.Next(1, 5));
            #endregion

            //Input tag and press Enter
            Thread.Sleep(1000);
            Tags.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Tags"));
            Tags.SendKeys(Keys.Enter);

            //Select Sevice Type
            Thread.Sleep(1000);
            ServiceTypeOptions.Click();

            //Select Location Type
            Thread.Sleep(1000);
            LocationTypeOption.Click();

            //Select Date 
            Thread.Sleep(3000);
            //StartDateDropDown.Clear();
            StartDateDropDown.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Startdate"));

            //Select End date
            Thread.Sleep(500);
            //EndDateDropDown.Clear();
            EndDateDropDown.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Enddate"));

            Thread.Sleep(2000);

            #region Select dates, days and time

            //Click on Selected days
            IList<IWebElement> Checkbox = Global.GlobalDefinitions.driver.FindElements(By.XPath("//input[@name='Available']"));

            //Click 0n Start time
            IList<IWebElement> StartTimeDropDown = Global.GlobalDefinitions.driver.FindElements(By.Name("StartTime"));

            //Click on End Time
            IList<IWebElement> EndTimeDropDown = Global.GlobalDefinitions.driver.FindElements(By.Name("EndTime"));
            if (Checkbox.Count != 0)
            {
                //Checkbox.WaitForElementClickable(Global.GlobalDefinitions.driver, 60);
                for (int i = 1; i <= Checkbox.Count - 2; i++)
                {
                    //Populate the excel data
                    GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkills");

                    Thread.Sleep(500);

                    if (!Checkbox.ElementAt(i).Selected)
                    {
                        Checkbox.ElementAt(i).Click();
                    }


                    //Select Start Time
                    Thread.Sleep(500);
                    //StartTimeDropDown.Clear();
                    StartTimeDropDown.ElementAt(i).SendKeys(GlobalDefinitions.ExcelLib.ReadData(i + 1, "Starttime"));

                    //Select End Time
                    Thread.Sleep(500);
                    //EndTimeDropDown.Clear();
                    EndTimeDropDown.ElementAt(i).SendKeys(GlobalDefinitions.ExcelLib.ReadData(i + 1, "Endtime"));
                }

            }
            #endregion

            #region select skill exchange or credit options
         
           IWebElement skillTradeRadio1 = driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[8]/div[2]/div/div[1]/div/input"));
            IWebElement skillTradeRadio2 = driver.FindElement(By.XPath("//*[@id='service-listing-section']/div[2]/div/form/div[8]/div[2]/div/div[2]/div/input"));

            if (skillTradeRadio1.Selected)
            {
                GlobalDefinitions.wait(20);
                Credit.Click();
                CreditAmount.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Credit"));
               
            }
            else
            {
                SkillExchange.Click();
                GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath, "ShareSkills");
                SkillExchange.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Skill-Exchange"));
                SkillExchange.SendKeys(Keys.Enter);
            }
            #endregion

            //Upload file
            GlobalDefinitions.wait(3);
            FileUpload.Click();
            GlobalDefinitions.wait(2);

            //Activate browse window
            AutoItX.WinWaitActive("Open");
            Thread.Sleep(1000);

            //Sets input focus to a given control on Open Window
            AutoItX.ControlFocus("Open", "", "Edit1");
            Thread.Sleep(1000);

            //// Sets text of a control.
            AutoItX.ControlSetText("Open", "", "Edit1", @"E:\Competition task\marsframework-master\marsframework-master\MarsFramework\ExcelData\Test.txt");
            Thread.Sleep(1000);

            //Sends a mouse click command to a given control.
            AutoItX.ControlClick("Open", "", "Button1");

            //Select active type
            Thread.Sleep(1000);
            ActiveOption.Click();

            //Save Share Skills Listing
            Save.WaitForElementClickable(Global.GlobalDefinitions.driver, 60);
            Save.Click();
            GlobalDefinitions.wait(7);
        }

        #endregion

        #region Validate if Skill saved successfully
        public void ValidateShareSkillSave()
        {
            Thread.Sleep(3000);
            string actualPageTitle = GlobalDefinitions.driver.Title;
            string expectedPageTitle = "ListingManagement";

            try
            {
                //Start the Reports
                Base.ExtentReports();
                Base.test = Base.extent.StartTest("Add a Service Listing");
                test.Log(LogStatus.Info, "Add a listing");
                GlobalDefinitions.wait(4);
                //verify if actual and expected page title are same
                Assert.AreEqual(actualPageTitle, expectedPageTitle);
                GlobalDefinitions.wait(10);
                test.Log(LogStatus.Pass, "Test Passed, Service listing added Successfully");
                SaveScreenShotClass.SaveScreenshot(GlobalDefinitions.driver, "SkillsAddedSuccessfully");
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
               GlobalDefinitions.wait(5);
              test.Log(LogStatus.Fail, "Test Failed");
              Assert.Fail("Test Failed, Skill not saved", ex.Message);
            }
        }
        #endregion

    }
}



    


