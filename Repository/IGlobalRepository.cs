using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroceryStoreAPI.Model;
namespace GroceryStoreAPI.Repository
{
    public interface IGlobalRepository
    {
        public int UpdateCustomerInformation(int id, string name);

        public int AddCustomerDetails(int id, string name);

        public List<Customer> RetrieveaCustomer(int id);

        public List<Customer> DeleteCustomerDetails(int id);
    }
}
