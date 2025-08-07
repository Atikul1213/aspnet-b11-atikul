using DevSkill.Inventory.Domain.Entities;

namespace DevSkill.Inventory.Domain.Services
{
    public interface ICustomerService
    {
        Task InsertCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Customer customer);
        Task<Customer> GetCustomerByIdAsync(Guid id);
        Task<IList<Customer>> GetAllCustomersAsync();
    }
}
