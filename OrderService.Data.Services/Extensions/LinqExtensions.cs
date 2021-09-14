using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace OrderService.Data.Services.Extensions
{
    public static class LinqExtensions
    {
        public static IMongoQueryable<T> OrderBy<T>(this IMongoQueryable<T> source, string propertyName) =>
            source.OrderBy(ToLambda<T>(propertyName));

        public static IMongoQueryable<T> OrderByDescending<T>(this IMongoQueryable<T> source, string propertyName) =>
            source.OrderByDescending(ToLambda<T>(propertyName));

        public static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T));
            var property = Expression.Property(parameter, propertyName);
            var propAsObject = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }

        public static IMongoQueryable<T> Page<T>(this IMongoQueryable<T> source, int pageSize, int page) =>
            source.Skip(--page * pageSize).Take(pageSize);
    }
}
