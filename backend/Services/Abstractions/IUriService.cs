using Domain;

namespace Services.Abstractions;

public interface IUriService
{
    Uri GetAllBooksUri(PaginationFilter paginationFilter = null);
}