using Domain.POCOs;
using Mapster;
using Repositories.Abstractions;
using Services.Abstractions;
using Services.Exceptions;
using Services.Localisations;
using Services.Models.ServiceModels;

namespace Services.Implementations;

public class OrderProcessor : IOrderProcessor
{
    private readonly IOrderRepository _orderRepository;
    private readonly IApartmentRepository _apartmentRepository;
    
    public OrderProcessor(IOrderRepository orderRepository, IApartmentRepository apartmentRepository)
    {
        _orderRepository = orderRepository;
        _apartmentRepository = apartmentRepository;
    }

    public async Task<int> BookAnApartment(OrderServiceModel orderRequest)
    {
        
        var apartment = await _apartmentRepository.GetByOwnerIdAsync(orderRequest.HostId);
        if(apartment is null)
                throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        
        var entities = await _orderRepository.GetActiveOrdersForApartment(apartment.Id);
        if (entities.Count != 0)
        {
            var check = CheckForDateAvailability(apartment.Adapt<ApartmentServiceModel>(), 
                entities.Adapt<List<OrderServiceModel>>(), orderRequest);

            if (check is false)
                throw new ApartmentNotAvailableException(ExceptionMessages.ApartmentNotAvailable);
        }

        orderRequest.IssuedAt = DateTime.Now;

        var id = await _orderRepository.CreateAsync(orderRequest.Adapt<Order>());
        return id;
    }

    public async Task<Order> ChangeBookingStatusAsync(int orderId, bool accepted)
    {
        var order = await _orderRepository.GetAsync(orderId);
        if(order is null)
                throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        
        var apartment = await _apartmentRepository.GetByOwnerIdAsync(order.HostId);
        if(apartment is null)
            throw new NotFoundException(ExceptionMessages.ObjectNotFound);
        
        var entities = await _orderRepository.GetActiveOrdersForApartment(apartment.Id);
        
        if (entities.Count != 0)
        {
            var check = CheckForDateAvailability(apartment.Adapt<ApartmentServiceModel>(), entities.Adapt<List<OrderServiceModel>>(),
                order.Adapt<OrderServiceModel>());

            if (check is false)
                throw new ApartmentNotAvailableException(ExceptionMessages.ApartmentNotAvailable);
        }

        await _orderRepository.ChangeOrderStatusAsync(orderId, accepted);
        return order;
    }

    public async Task<Order> CancelABooking(int orderId)
    {
        throw new NotImplementedException();
    }

    #region Private Methods
    private bool CheckForDateAvailability(ApartmentServiceModel apartment, List<OrderServiceModel> entities,
                                        OrderServiceModel orderRequest)
    {
        var busyDates = new List<DateTime>();
        foreach (var entity in entities)
        {
            busyDates.AddRange(GetAllDaysFromRange(entity.From, entity.To));
        }

        var requestDates = GetAllDaysFromRange(orderRequest.From, orderRequest.To);

        foreach (var item in requestDates)
        {
            if (busyDates.Contains(item))
                return false;
        }

        return true;
    }
    
    private List<DateTime> GetAllDaysFromRange(DateTime from, DateTime to)
    {
        var list = new List<DateTime>();
        for (var dt = from; dt <= to; dt = dt.AddDays(1))
        {
            list.Add(dt);
        }

        return list; 
    }
    #endregion
}