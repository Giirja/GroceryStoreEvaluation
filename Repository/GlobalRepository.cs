using GroceryStoreAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace GroceryStoreAPI.Repository
{
    public class GlobalRepository : IGlobalRepository
    {
        List<Customer> cusDetail = null;
        Root deserializedCustomerClass = null;
        int id = 0;
        string name = string.Empty;
        string json = string.Empty;

        #region CRUD functions
        /// <summary>
        /// Method to Update Customer Information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public int UpdateCustomerInformation(int id, string name)
        {

            Customer tempCustomer;
            List<Customer> lsttempCustomerDetails = new List<Customer>();
            deserializedCustomerClass = FetchCustomerDetails();
            if (deserializedCustomerClass != null)
            {
                cusDetail = deserializedCustomerClass.customers;
                foreach (Customer customerfetch in cusDetail)
                {
                    if (customerfetch.id == id)
                    {
                        tempCustomer = new Customer();
                        tempCustomer.id = Convert.ToInt32(customerfetch.id);
                        tempCustomer.name = name;
                        lsttempCustomerDetails.Add(tempCustomer);
                    }
                    else
                    {
                        tempCustomer = new Customer();
                        tempCustomer.id = Convert.ToInt32(customerfetch.id);
                        tempCustomer.name = customerfetch.name;
                        lsttempCustomerDetails.Add(tempCustomer);
                    }
                }
            }

            deserializedCustomerClass.customers = lsttempCustomerDetails;
            UpdateCustomertoDB(deserializedCustomerClass);
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(deserializedCustomerClass);
            //System.IO.File.WriteAllText("database.json", json);
            return 1;
        }

       

        /// <summary>
        /// Code to add New Customer Details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public int AddCustomerDetails(int id, string name)
        {
            deserializedCustomerClass = FetchCustomerDetails();
            string json = string.Empty;

            if (deserializedCustomerClass != null && deserializedCustomerClass.customers.Count > 0)
            {
                if (!deserializedCustomerClass.customers.Any(x => x.id.Equals(id)))
                {
                    Customer customer = new Customer();
                    customer.id = id;
                    customer.name = name;
                    deserializedCustomerClass.customers.Add(customer);
                }
                else
                {
                    return 2;
                }

            }

            UpdateCustomertoDB(deserializedCustomerClass);
            return 1;
        }

       

        /// <summary>
        /// Code written to fetch a Customer Info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Customer> RetrieveaCustomer(int id)
        {
            deserializedCustomerClass = FetchCustomerDetails();
            cusDetail = deserializedCustomerClass.customers.Where(x => x.id.Equals(id)).ToList();
            return cusDetail;
        }

        /// <summary>
        /// Code written to Delete CustomerDetails
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Customer> DeleteCustomerDetails(int id)
        {
            deserializedCustomerClass = FetchCustomerDetails();
            if (deserializedCustomerClass != null && deserializedCustomerClass.customers != null &&
                deserializedCustomerClass.customers.Count > 0)
            {
                var index = deserializedCustomerClass.customers.FindIndex(x => x.id == id);
                if (index >= 0) deserializedCustomerClass.customers.RemoveAt(index);
            }
            UpdateCustomertoDB(deserializedCustomerClass);
            cusDetail = deserializedCustomerClass.customers;
            return cusDetail;
        }

        #endregion
        #region Common Code

        /// <summary>
        /// Common code written to fetch Customer details from JSON file
        /// </summary>
        /// <returns></returns>
        Root FetchCustomerDetails()
        {

            string json = string.Empty;
            using (StreamReader r = new StreamReader("database.json"))
            {
                json = r.ReadToEnd();
                deserializedCustomerClass = JsonConvert.DeserializeObject<Root>(json);
            }
            return deserializedCustomerClass;
        }

        /// <summary>
        /// Common code written to update Customer details to DB
        /// </summary>
        /// <param name="deserializedCustomerClass"></param>
        public void UpdateCustomertoDB(Root deserializedCustomerClass)
        {
            json = Newtonsoft.Json.JsonConvert.SerializeObject(deserializedCustomerClass);
            System.IO.File.WriteAllText("database.json", json);
        }

        #endregion'
    }


}
