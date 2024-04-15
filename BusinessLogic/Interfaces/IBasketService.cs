using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IBasketService
    {
        IEnumerable<ProductDto> GetProducts();
        IEnumerable<int> GetProductsIds();

        void AddProduct(int id);
        void Remove(int id);
        int GetCount();
        bool isExist(int id);      
    }
}
