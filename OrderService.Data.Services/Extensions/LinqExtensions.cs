using System;
using System.Linq;
using System.Linq.Expressions;

namespace OrderService.Data.Services.Extensions
{
    public static class LinqExtensions
    {
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName) =>
            source.OrderBy(ToLambda<T>(propertyName));

        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName) =>
            source.OrderByDescending(ToLambda<T>(propertyName));

        public static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> source, int pageSize, int page) =>
            source.Skip(page * pageSize).Take(pageSize);
    }
}
