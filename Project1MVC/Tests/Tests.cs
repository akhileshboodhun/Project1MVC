using NUnit.Framework;
using Project1MVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project1MVC.Tests
{
    public class Tests
    {
        [TestCase("de")]
        [TestCase("desc")]
        [TestCase("descending")]
        [TestCase("degffgbgf")]
        public void SanitizeSortOrderForDescending(string sortOrder)
        {
            string result = ServicesHelper.SanitizeSortOrder(sortOrder);
            Assert.That(result, Is.EqualTo("desc"));
        }

        [TestCase("asc")]
        [TestCase("ascend")]
        [TestCase("degffgbgf")]
        [TestCase(" ")]
        [TestCase("")]
        public void SanitizeSortOrderForAscending(string sortOrder)
        {
            var result = ServicesHelper.SanitizeSortOrder(sortOrder);
            Assert.That(result, Is.EqualTo("asc"));
        }
    }
}