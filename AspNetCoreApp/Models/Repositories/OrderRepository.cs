using System;

namespace AspNetCoreApp.Models.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _db;
        private readonly ShoppingCart _shoppingCart;


        public OrderRepository(AppDBContext appDbContext, ShoppingCart shoppingCart)
        {
            _db = appDbContext;
            _shoppingCart = shoppingCart;
        }


        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _db.Orders.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    OrderId = order.OrderId,
                    Price = shoppingCartItem.Pie.Price
                };

                _db.OrderDetails.Add(orderDetail);
            }

            _db.SaveChanges();
        }
    }
}
