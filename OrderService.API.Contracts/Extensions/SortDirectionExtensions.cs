using System;
using System.ComponentModel;

namespace OrderService.API.Contracts.Extensions
{
    internal static class SortDirectionExtensions
    {
        private static readonly string Ascending;
        private static readonly string Descending;

        internal static ListSortDirection ToListSortDirection(this string sortDirection)
        {
            if (string.Equals(sortDirection, Ascending, StringComparison.InvariantCultureIgnoreCase))
                return ListSortDirection.Ascending;

            if (string.Equals(sortDirection, Descending, StringComparison.InvariantCultureIgnoreCase))
                return ListSortDirection.Descending;

            throw new Exception();
        }
    }
}
