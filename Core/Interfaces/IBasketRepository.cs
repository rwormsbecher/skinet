namespace Core;

public interface IBasketRepository
{
    Task<CustomerBasket> GetBasketAsync(string basketId);
    Task<CustomerBasket> UpdateBasketasync(CustomerBasket basket);

    Task<bool> DeleteBasketAsync(string basketId);
}
