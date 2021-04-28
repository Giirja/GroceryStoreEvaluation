using NUnit.Framework;
using GroceryStoreAPI;

namespace UnitTesting
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_MainPage_Positive()
        {

            CustomerController customerController = new CustomerController();
            var dic = new System.Collections.Generic.Dictionary<string, Microsoft.Extensions.Primitives.StringValues>();
            dic.Add("txtId", "1");
            dic.Add("txtName", "test1");
            var collection = new Microsoft.AspNetCore.Http.FormCollection(dic);
            var actionresult = customerController.Index(collection);

            Assert.AreEqual(1, ((Microsoft.AspNetCore.Mvc.ViewResult)actionresult).ViewData.Count);

        }

        // Method written to check if it throws exception when formcollection is send as null in the input

        [Test]
        public void Test_IndexAction__Negative()
        {

            CustomerController customerController = new CustomerController();
            var dic = new System.Collections.Generic.Dictionary<string, Microsoft.Extensions.Primitives.StringValues>();

            var collection = new Microsoft.AspNetCore.Http.FormCollection(null);
            var actionresult = customerController.Index(collection);

            Assert.AreEqual(1, ((Microsoft.AspNetCore.Mvc.ViewResult)actionresult).ViewData.Count);

        }

        [Test]
        public void Test_IndexAction__WithDBnull_Negative()
        {

            CustomerController customerController = new CustomerController();
            var dic = new System.Collections.Generic.Dictionary<string, Microsoft.Extensions.Primitives.StringValues>();

            var collection = new Microsoft.AspNetCore.Http.FormCollection(null);
            var actionresult = customerController.Index(collection);

            Assert.AreEqual(1, ((Microsoft.AspNetCore.Mvc.ViewResult)actionresult).ViewData.Count);

        }
    }
}
