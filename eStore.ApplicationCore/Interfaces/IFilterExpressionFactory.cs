using System;
using System.Linq.Expressions;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Interfaces
{
    public interface IFilterExpressionFactory<T> where T : Goods
    {
        Expression<Func<T, bool>> CreateExpression(GoodsFilterModel filterModel);
    }
}