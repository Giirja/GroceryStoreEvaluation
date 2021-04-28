using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using GroceryStoreAPI.Model;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using GroceryStoreAPI.Repository;
using Newtonsoft.Json;
namespace GroceryStoreAPI
{
    public class CustomerController : Controller
    {
        GlobalRepository iGlobalRepository = new GlobalRepository();

        //Main method to load the list of Customers in DB
        //Has inner methods to call various CRUD operation
        // GET: /<controller>/
        public ActionResult Index(Microsoft.AspNetCore.Http.IFormCollection collection)
        {
            try
            {
                if (collection != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (collection["txtId"] != string.Empty || collection["txtName"] != string.Empty)
                        {
                            int id = Convert.ToInt32(collection["txtId"]);
                            string name = collection["txtName"];
                            if (collection["buttoncheck"] != string.Empty)
                            {
                                switch (collection["buttoncheck"])
                                {
                                    case "Update":
                                        UpdatingCustomerDetail(id, name);
                                        break;
                                    case "Add":
                                        AddCustomerDetails(id, name);
                                        break;
                                    case "Retrieve":
                                        RetrieveaCustomer(id);
                                        break;
                                }

                            }
                        }
                    }
                }
                Root deserializedCustomerClass = null;
                string json = string.Empty;


                using (StreamReader r = new StreamReader("database.json"))
                {
                    json = r.ReadToEnd();
                    deserializedCustomerClass = JsonConvert.DeserializeObject<Root>(json);
                }
                if (collection["buttoncheck"] != "Retrieve")
                {
                    ViewBag.CustomerDetails = deserializedCustomerClass.customers;
                }

                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Method to add new customers 
        public ActionResult AddCustomerDetails(int id, string name)
        {
            iGlobalRepository.AddCustomerDetails(id, name);
            return RedirectToAction("Index", "Customer");
        }

        //Method to retrieve a customer
        public ActionResult RetrieveaCustomer(int Id)
        {
            if (ModelState.IsValid)
            {
                List<Customer> cusDetail = iGlobalRepository.RetrieveaCustomer(Id);

                if (cusDetail != null)
                {
                    ViewBag.CustomerDetails = cusDetail;
                }
            }
            return Redirect("/Customer/Index");

        }


        //Method to update a customer
        ActionResult UpdatingCustomerDetail(int id, string name)
        {
            int UpdateStatus = iGlobalRepository.UpdateCustomerInformation(id, name);

            return Redirect("/Customer/Index");
        }

        //Method to delete a customer
        public ActionResult DeleteCustomerDetails(int id)
        {
            List<Customer> cusDetail = iGlobalRepository.DeleteCustomerDetails(id);
            if (cusDetail != null && cusDetail.Count > 0)
                ViewBag.CustomerDetails = cusDetail;
            return Redirect("/Customer/Index");
        }


    }
}
