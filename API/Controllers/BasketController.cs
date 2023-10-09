using Core;
using Microsoft.AspNetCore.Mvc;

namespace API;

public class BasketController : BaseApiController
{
    private readonly IBasketRepository basketRepository;

    public BasketController(IBasketRepository basketRepository)
    {
        this.basketRepository = basketRepository;
    }

    [HttpGet]
    public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
    {
        var basket = await basketRepository.GetBasketAsync(id);

        return Ok(basket ?? new CustomerBasket(id));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
    {
        var updatedBasket = await basketRepository.UpdateBasketasync(basket);

        return Ok(updatedBasket);
    }


    [HttpDelete]
    public async Task DeleteBasketAsync(string id)
    {
        await basketRepository.DeleteBasketAsync(id);
    }
}
