using System.Linq.Expressions;
using eStore.ApplicationCore.FilterModels;

namespace eStore.ApplicationCore.Factory
{
    public interface IExpressionFactory
    {
        Expression CreateFilterExpression(GoodsFilterModel filterModel);
    }
}