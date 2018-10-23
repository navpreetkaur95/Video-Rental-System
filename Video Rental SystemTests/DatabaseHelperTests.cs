using Microsoft.VisualStudio.TestTools.UnitTesting;
using Video_Rental_System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Video_Rental_System.Tests
{
    [TestClass()]
    public class DatabaseHelperTests
    {
        [TestMethod()]
        //to test the database connection 
        public void DatabaseConnectionTest()
        {
            try
            {
                DatabaseHelper dh = new DatabaseHelper();
                // first senario
                string result = dh.DatabaseConnection();
                Assert.AreEqual("Connected", result);

                // second senario
                /*
                dh = new DatabaseHelper("asd");
                result = dh.DatabaseConnection();
                Assert.AreEqual("Connected", result);
                */

            }
            catch (Exception ex)
            {
                Console.WriteLine("Test Failed: " + ex.ToString());
                Assert.Fail();
            }
        }

        [TestMethod()]
        // to test if charges are calculated correctly
        public void CalculateChargeTest()
        {
            try
            {
                DatabaseHelper dh = new DatabaseHelper();
            int rate = dh.CalculateCharge(2015);
            Assert.AreEqual(2, rate);
            rate = dh.CalculateCharge(2000);
            Assert.AreEqual(5, rate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Test Failed: " + ex.ToString());
                Assert.Fail();
            }
        }
    }
}