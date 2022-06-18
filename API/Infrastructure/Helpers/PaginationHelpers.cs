using API.Contracts.Queries;
using API.Contracts.Responses;
using Domain;
using Mapster;
using Services.Abstractions;

namespace API.Infrastructure.Helpers;

public static class PaginationHelpers
{
    public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationQuery pagination, List<T> objs)
    {
        var nextPage = pagination.PageNumber >= 1
            ? uriService.GetAllBooksUri(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize)
                .Adapt<PaginationFilter>()).ToString()
            : null;
        
        var previousPage = pagination.PageNumber - 1 >= 1
            ? uriService.GetAllBooksUri(new PaginationQuery(pagination.PageNumber -1, pagination.PageSize)
                .Adapt<PaginationFilter>()).ToString()
            : null;
        
        var paginationResponse = new PagedResponse<T>
        {
            Data = objs,
            PageNumber = pagination.PageNumber>= 1?pagination.PageNumber : null,
            PageSize = pagination.PageSize >= 1 ? pagination.PageSize : null,
            NextPage = objs.Any() ? nextPage:null,
            PreviousPage = previousPage
        };

        return paginationResponse;
    }
}