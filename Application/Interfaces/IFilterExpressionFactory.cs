using System;
using System.Linq.Expressions;
using eStore.Application.FilterModels;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces
{
    public interface IFilterExpressionFactory<T> where T : Goods
    {
        Expression<Func<T, bool>> CreateExpression(GoodsFilterModel filterModel);
    }
}