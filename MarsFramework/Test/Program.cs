using MarsFramework.Global;
using MarsFramework.Pages;
using NUnit.Framework;
using System;

namespace MarsFramework
{
    public class Program
    {
        [TestFixture]
        [Category("Sprint1")]
        class User : Global.Base
        {

            [Test, Order(1)]
            [Obsolete]
            public void EnterShareSkillTest()
            {

               // SignIn signInObj = new SignIn();
                //signInObj.LoginSteps();

                ShareSkill shareSkillObj = new ShareSkill();
                shareSkillObj.EnterShareSkill();
                shareSkillObj.ValidateShareSkillSave();
               
                
            }

            [Test, Order(2)]
            [Obsolete]
            public void EditServiceListingTest()
            {

                ManageListings manageListingsObj = new ManageListings();
                manageListingsObj.EditServiceListing();
                manageListingsObj.ValidateEditServiceListing();

            }

            [Test, Order(3)]
            [Obsolete]
            public void DeleteListing()
            {


                ManageListings manageListingObj = new ManageListings();
                manageListingObj.DeleteListings();
                manageListingObj.ValidateDeleteListing();

            }

        }
    }
}