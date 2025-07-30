using System.Linq.Expressions;
using Application.Dtos.Folders;

namespace Application.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyDynamicFilter<T>(this IQueryable<T> query, DynamicFilterSortDto filter)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, filter.FilterBy);
        var targetType = Nullable.GetUnderlyingType(property.Type) ?? property.Type;
        var typedValue = Convert.ChangeType(filter.Value, targetType);
        var constant = Expression.Constant(typedValue, property.Type);

        Expression? comparison = filter.Operation switch
        {
            FilterOperation.Equal => Expression.Equal(property, constant),
            FilterOperation.NotEqual => Expression.NotEqual(property, constant),
            FilterOperation.GreaterThan => Expression.GreaterThan(property, constant),
            FilterOperation.LessThan => Expression.LessThan(property, constant),
            FilterOperation.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, constant),
            FilterOperation.LessThanOrEqual => Expression.LessThanOrEqual(property, constant),
            _ => throw new NotSupportedException("Unsupported filtering")
        };

        var lambda = Expression.Lambda<Func<T, bool>>(comparison!, parameter);
        return query.Where(lambda);
    }

    public static IQueryable<T> ApplyDynamicSorting<T>(this IQueryable<T> query, DynamicFilterSortDto filter)
    {
        if (string.IsNullOrEmpty(filter.SortBy))
            return query;

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, filter.SortBy);
        var lambda = Expression.Lambda(property, parameter);
        string methodName = filter.SortDescending == null ? "OrderBy" : "OrderByDescending";

        var result = typeof(Queryable).GetMethods()
            .First(method => method.Name == methodName && method.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), property.Type)
            .Invoke(null, new object[] { query, lambda });

        return (IQueryable<T>)result!;
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int pageSize)
    {
        var items = query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        // var totalCount = query.Count();

        return items;
    }
}