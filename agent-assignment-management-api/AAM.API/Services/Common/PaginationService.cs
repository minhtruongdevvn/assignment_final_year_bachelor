using AAM.Application;
using AutoMapper;
using Fluorite.Extensions;
using Fluorite.Strainer.Models;
using Fluorite.Strainer.Services;
using Microsoft.EntityFrameworkCore;

namespace AAM.API;

public abstract class PaginationService<TEntity, TDTO> where TDTO : class
{
    readonly IQueryable<TEntity> _query;
    readonly IConfiguration _configuration;
    readonly IStrainerProcessor _processor;
    readonly IMapper _mapper;
    public PaginationService(
        IQueryable<TEntity> query,
        IConfiguration configuration,
        IStrainerProcessor processor,
        IMapper mapper)
    {
        _query = query;
        _configuration = configuration;
        _processor = processor;
        _mapper = mapper;
    }

    public async Task<PagedResult<TDTO>> GeneratePageResultAsync(StrainerModel model)
    {
        var dataQuery = _query.Apply(model, _processor);
        var result = new PagedResult<TDTO>();
        result.CurrentPage = model.Page ?? _configuration.GetValue<int>("Strainer:DefaultPageNumber");
        result.PageSize = model.PageSize ?? _configuration.GetValue<int>("Strainer:DefaultPageSize");
        result.RowCount = await
            _query
            .Apply
            (
                new StrainerModel { Filters = model.Filters },
                _processor,
                applyPagination: false,
                applySorting: false
            )
            .CountAsync();
        var pageCount = (double)result.RowCount / result.PageSize;
        result.PageCount = (int)Math.Ceiling(pageCount);
        result.Results = _mapper.Map<List<TDTO>>(await dataQuery.ToListAsync());


        return result;
    }
}

