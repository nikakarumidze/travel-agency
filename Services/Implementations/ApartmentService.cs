using Mapster;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Exceptions;
using Services.Localisations;
using Services.Models;

namespace Services.Implementations;

public class ApartmentService : IApartmentService
{
    private readonly IApartmentRepository _apartmentRepository;

    public ApartmentService(IApartmentRepository apartmentRepository)
    {
        _apartmentRepository = apartmentRepository;
    }


    public async Task<ApartmentServiceModel> GetMyApartmentAsync(string id)
    {
        var obj = await _apartmentRepository.GetByOwnerIdAsync(id);
        if (obj == null)
            throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        return obj.Adapt<ApartmentServiceModel>();
    }

    public async Task<int> CreateApartmentAsync(ApartmentServiceModel request)
    {
        throw new NotImplementedException();
    }
}