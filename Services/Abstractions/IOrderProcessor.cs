using Domain.POCOs;
using Services.Models;
using Services.Models.ServiceModels;

namespace Services.Abstractions;

public interface IOrderProcessor
{
    Task<int> BookAnApartment(OrderServiceModel order);
    Task<Order> ChangeBookingStatusAsync(int orderId, bool accepted);
    Task<Order> CancelABooking(int orderId);
}