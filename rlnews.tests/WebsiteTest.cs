using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rlnews.Controllers;

namespace rlnews.tests
{
    /// <summary>
    /// Unit test class for rlnews.website
    /// </summary>
    [TestClass]
    public class WebsiteTest
    {
        /// <summary>
        /// Tests the account controller password hash method
        /// </summary> 
        [TestMethod]
        public void TestPasswordHash()
        {
            AccountController account = new AccountController();

            string input = "password";
            string salt = "test";
            string exceptedResult = "a7574a42198b7d7eee2c037703a0b95558f195457908d6975e681e2055fd5eb9";
            string actualResult = account.GenerateHash(input, salt);

            Console.WriteLine("Expected: " + exceptedResult);
            Console.WriteLine("Result: " + actualResult);

            Assert.AreEqual(exceptedResult, actualResult, "Hashes match.");
        }

        /// <summary>
        /// Tests the account controller random salt method
        /// </summary> 
        [TestMethod]
        public void TestPasswordSalt()
        {
            AccountController account = new AccountController();

            string exceptedResult = account.GenerateSalt(10);
            string actualResult = account.GenerateSalt(10);

            Console.WriteLine("Expected: " + exceptedResult);
            Console.WriteLine("Result: " + actualResult);

            Assert.AreNotEqual(exceptedResult, actualResult, "Random salts successfully generated.");
        }
    }
}
