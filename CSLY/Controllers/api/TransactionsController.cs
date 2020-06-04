using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CSLY.Models;
using CSLY.Repository;
using CSLY.Dtos;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using CSLY.MyAuthorizations;

namespace CSLY.Controllers.api
{
    [JwtAuthorize]
    public class TransactionsController : ApiController
    {
        private ApplicationUserManager _userManager;

        private GenericUnitofWork dbContext = new GenericUnitofWork();


        public TransactionsController()
        {

        }

        public TransactionsController(ApplicationUserManager userManager)
        {

            UserManager = userManager;
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [HttpPost]
        public IHttpActionResult RequestOrder(TransactionsDto transDto)
        {
            Order myFirstOrder;
            IEnumerable<OrderItem> myAllOrders;

            if (!ModelState.IsValid)
                return BadRequest();

            IEnumerable<ProductItem> Products = transDto.ProductItems.ToList();

            //get client_id using transDto.username
            int client_id = UserManager.Users
                .Where(s => s.UserName == transDto.UserName)
                .Select(s => s.Client_Id).FirstOrDefault();

            //check if it's the first order in this day or not
            var checkClientFirstOrder = dbContext.GetRepositoryInstance<Order>()
                .GetFirstOrDefaultByParam(ord => ord.OrderDate == DateTime.Today && ord.ClientId == client_id);

            //if it's first order for this day
            //save client in Order table
            if (checkClientFirstOrder == null)
            {
                myFirstOrder = new Order { OrderDate = DateTime.Today, ClientId = client_id };
                dbContext.GetRepositoryInstance<Order>().Add(myFirstOrder);
                //if it's first order today so we need to save value of myFirstOrder.id in
                //checkClientFirstOrder.Id to add in later in orderitem table
                checkClientFirstOrder = myFirstOrder;
            }

            //Update Products.Amount for all products
            myAllOrders = UpdateProductsToDb(transDto.ProductItems , checkClientFirstOrder.Id);

            //add all orders in orderitem table with orderId for this day
            //so we can keep track of each client orders per day
            dbContext.GetRepositoryInstance<OrderItem>().AddRange(myAllOrders);

            dbContext.Complete();

            return Ok(checkClientFirstOrder);
        }

        private IEnumerable<OrderItem> UpdateProductsToDb(IEnumerable<ProductItem> myProductItems , int orderId)
        {
            List<OrderItem> myOrderItemList = new List<OrderItem>();
            foreach(ProductItem pItem in myProductItems)
            {
                var productInDb = dbContext.GetRepositoryInstance<Product>()
                    .GetFirstOrDefaultByParam(p => p.ProductName == pItem.ProductName);

                if (productInDb.Amount >= pItem.Quantity)
                {
                    productInDb.Amount -= pItem.Quantity;
                    dbContext.Complete();
                    myOrderItemList.Add(new OrderItem
                    { OrderId = orderId , ProductId = productInDb.ProductId , Quantity = pItem.Quantity , Price = pItem.Price});
                }
            }
            return myOrderItemList;
        }
    }
}
