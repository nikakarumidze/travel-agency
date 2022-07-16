using Domain;
using Microsoft.AspNetCore.WebUtilities;
using Services.Abstractions;

namespace Services.Implementations;

public class UriService : IUriService
{
    private readonly string _baseUri;

    public UriService(string baseUri)
    {
        _baseUri = baseUri;
    }

    public Uri GetAllBooksUri(PaginationFilter pagination = null)
    {
        var uri = new Uri(_baseUri);

        if (pagination == null)
            return uri;

        var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pagination.PageNumber.ToString());
        modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());
        return new Uri(modifiedUri);
    }
}