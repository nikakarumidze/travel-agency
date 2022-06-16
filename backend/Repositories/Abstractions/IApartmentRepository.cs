using Domain.POCOs;
using Repositories.Models;

namespace Repositories.Abstractions;

public interface IApartmentRepository
{
    Task<Apartment> GetAsync(int id);
    Task<Apartment> GetByOwnerUsernameAsync(string username);
    Task<Apartment> GetByOwnerIdAsync(string id);
    Task<List<Apartment>> GetAllAsync();
    Task<List<Apartment>> GetAllByCityAsync(string city);
    Task<List<Apartment>> GetByAddressAsync(string address);
    Task<List<Apartment>> GetAllWithOwnersAsync();
    Task<Apartment> GetWithOwnerAsync(int id);
    Task<int> CreateAsync(Apartment apartment);
    Task UpdateAsync(Apartment apartment);
    Task DeleteAsync(int id);

    Task<List<Apartment>> SearchAsync(ApartmentSearchModel model);
}