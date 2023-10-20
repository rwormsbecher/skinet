using System.Security.Claims;
using AutoMapper;
using Core;
using Core.OrderAggregate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class OrderController : BaseApiController
{
    private readonly IOrderService orderService;
    private readonly IMapper mapper;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        this.orderService = orderService;
        this.mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
    {
        var email = HttpContext.User.RetrieveEmailFromPrincipal();

        var address = mapper.Map<AddressDto, AddressOrder>(orderDto.ShipToAddress);

        var order = await orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

        if (order == null)
        {
            return BadRequest(new ApiResponse(400, "Problem creating order"));
        }

        return Ok(order);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
    {
        var email = HttpContext.User.RetrieveEmailFromPrincipal();
        var orders = await orderService.GetOrdersForUserAsync(email);

        return Ok(mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
    {
        var email = HttpContext.User.RetrieveEmailFromPrincipal();

        var order = await orderService.GetOrderByIdAsync(id, email);

        if (order == null) return NotFound(new ApiResponse(404));

        return mapper.Map<OrderToReturnDto>(order);
    }

    [HttpGet("deliveryMethods")]
    public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
    {
        return Ok(await orderService.GetDeliveryMethodsAsync());
    }
}
