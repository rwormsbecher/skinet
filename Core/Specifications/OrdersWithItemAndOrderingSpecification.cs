﻿using System.Linq.Expressions;

namespace Core;

public class OrdersWithItemAndOrderingSpecification : BaseSpecification<Order>
{
    public OrdersWithItemAndOrderingSpecification(string email) : base(o => o.BuyerEmail == email)
    {
        AddInclude(o => o.OrderItems);
        AddInclude(o => o.DeliveryMethod);
        AddOrderByDescending(o => o.OrderDate);
    }

    public OrdersWithItemAndOrderingSpecification(int id, string email) : base(o => o.Id == id && o.BuyerEmail == email)
    {
        AddInclude(o => o.OrderItems);
        AddInclude(o => o.DeliveryMethod);
    }
}
