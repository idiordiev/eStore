using System;
using System.Linq.Expressions;
using eStore.Application.Filtering.Models.Shared;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces
{
    public interface IFilterExpressionFactory<TGoods, TFilterModel> where TGoods : Goods
        where TFilterModel : GoodsFilterModel
    {
        Expression<Func<TGoods, bool>> GetExpression(TFilterModel filterModel);
    }
}