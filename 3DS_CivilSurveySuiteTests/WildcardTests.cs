﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace _3DS_CivilSurveySuiteTests
{
    [TestClass]
    public class WildcardTests
    {
        [TestMethod]
        public void Test_Wildcard_Regex1()
        {
            string deskey = "FP#*";
            string pattern = "\\A" + deskey.Replace("#", "\\d\\d?\\d?").Replace("*", ".*");
            //string expectedPattern = "\\AFP\\d.*";


            //Assert.AreEqual(expectedPattern, pattern);


            string[] rawDesTrue = { "FP1 CONCRETE", "FP1S CONCRETE", "FP11S CONCRETE" };
            for (int i = 0; i < rawDesTrue.Length; i++)
            {
                Assert.AreEqual(true, Regex.Match(rawDesTrue[i], pattern).Success);
            }

            string[] rawDesFalse = { "FPH1 CONCRETE", "FPH1S CONCRETE", "CONCRETE FP11S CONCRETE" };
            for (int i = 0; i < rawDesFalse.Length; i++)
            {
                Assert.AreEqual(false, Regex.Match(rawDesFalse[i], pattern).Success);
            }

        }

        [TestMethod]
        public void Test_Wildcard_Capture_Group()
        {
            string deskey = "FP#*";
            string pattern = "^" + deskey.Replace("#", "(\\d\\d?\\d?)").Replace("*", ".*");

            string expectedPattern = "^FP(\\d\\d?\\d?).*";
            Assert.AreEqual(expectedPattern, pattern); //check pattern match

            string rawDes = "FP12 CONCRETE";
            string expectedNumber = "12";

            var match = Regex.Match(rawDes, pattern, RegexOptions.IgnoreCase);

            Assert.AreEqual(expectedNumber, match.Groups[1].Value);

        }
    }
}
