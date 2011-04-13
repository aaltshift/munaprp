namespace nothinbutdotnetprep.infrastructure.sorting
{
    using System;
    using System.Collections.Generic;

    public static class IEnumerableForSortingExtensions
    {
        public static ComparerChainer<ItemToSort> order_by<ItemToSort, PropertyType>(
            this IEnumerable<ItemToSort> collection, PropertyAccessor<ItemToSort, PropertyType> accessor)
            where PropertyType : IComparable<PropertyType>
        {
            return new ComparerChainer<ItemToSort>(collection, Sort<ItemToSort>.by(accessor));
        }

        public static ComparerChainer<ItemToSort> order_by<ItemToSort, PropertyType>(
            this IEnumerable<ItemToSort> collection, PropertyAccessor<ItemToSort, PropertyType> accessor, params PropertyType[] values)
        {
            return new ComparerChainer<ItemToSort>(collection, Sort<ItemToSort>.by(accessor, values));
        }
    }
}