using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using Core.Common.Result;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    public async Task<Result<Pagination<TResult>>> CreatePagination<T, TResult>(IGenericRepository<T> repo,
        ISpecification<T, TResult> spec, int pageNumber, int pageSize) where T : BaseEntity
    {
        var items = await repo.GetAllAsync(spec);
        var count = await repo.CountAsync(spec);
        var page = new Pagination<TResult>(items, pageSize, pageNumber, count);

        return page.Items != null ? Result.Success(page) : Result.Failure<Pagination<TResult>>(GenericErrors.OperationError);

    }
}
