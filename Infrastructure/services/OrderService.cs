using System.Linq;
using Core;
using Core.Entities;
using Core.OrderAggregate;

namespace Infrastructure;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IBasketRepository basketRepo;

    public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
    {

        this.unitOfWork = unitOfWork;
        this.basketRepo = basketRepo;
    }

    public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, AddressOrder shippingaddress)
    {
        // get basket from the basketrepo
        var basket = await basketRepo.GetBasketAsync(basketId);

        // get items from product repo
        var items = new List<OrderItem>();
        foreach (var item in basket.Items)
        {
            var productItem = await unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
            var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
            var orderItem = new OrderItem(itemOrdered, productItem.Price, item.Quantity);
            items.Add(orderItem);
        }

        // get delivery method
        var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

        // calc subtotal
        var subtotal = items.Sum(item => item.Price * item.Quantity);


        // create order
        var order = new Order(items, buyerEmail, shippingaddress, deliveryMethod, subtotal);
        unitOfWork.Repository<Order>().Add(order);

        // save to db
        var results = await unitOfWork.Complete();

        if (results <= 0)
        {
            return null;
        }

        await basketRepo.DeleteBasketAsync(basketId);

        // return order
        return order;
    }

    public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
    {
        return await unitOfWork.Repository<DeliveryMethod>().ListallAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
    {
        var spec = new OrdersWithItemAndOrderingSpecification(id, buyerEmail);
        return await unitOfWork.Repository<Order>().GetEntityWithSpec(spec);

    }

    public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
    {
        var spec = new OrdersWithItemAndOrderingSpecification(buyerEmail);
        return await unitOfWork.Repository<Order>().ListAsync(spec);
    }
}
