﻿using System;
using System.Collections.Generic;

using nothinbutdotnetprep.infrastructure.filtering;

namespace nothinbutdotnetprep.infrastructure.sorting
{
    public class ComparerBuilder<ItemToSort> : IComparer<ItemToSort>
    {
        private readonly IComparer<ItemToSort> comparer;

        public ComparerBuilder()
        {
        }

        public ComparerBuilder(IComparer<ItemToSort> currentComparer, IComparer<ItemToSort> newComparer)
        {
            this.comparer = currentComparer != null ? new ChainedComparer<ItemToSort>(currentComparer, newComparer) : newComparer;
        }

        public int Compare(ItemToSort x, ItemToSort y)
        {
            return this.comparer.Compare(x, y);
        }

        public ComparerBuilder<ItemToSort> then_by<PropertyType>(PropertyAccessor<ItemToSort, PropertyType> accessor,
                                                                 params PropertyType[] fixed_order)
        {
            return new ComparerBuilder<ItemToSort>(this.comparer, new FixedOrderComparer<ItemToSort, PropertyType>(accessor, fixed_order));
        }

        public ComparerBuilder<ItemToSort> then_by<PropertyType>(PropertyAccessor<ItemToSort, PropertyType> accessor)
            where PropertyType : IComparable<PropertyType>
        {
            return new ComparerBuilder<ItemToSort>(this.comparer, this.create_anonymous_comparer(accessor));
        }

        public ComparerBuilder<ItemToSort> then_by_descending<PropertyType>(PropertyAccessor<ItemToSort, PropertyType> accessor)
            where PropertyType : IComparable<PropertyType>
        {
            return new ComparerBuilder<ItemToSort>(this.comparer, this.create_reverse_comparer(accessor));
        }

        private IComparer<ItemToSort> create_anonymous_comparer<PropertyType>(PropertyAccessor<ItemToSort, PropertyType> accessor)
            where PropertyType : IComparable<PropertyType>
        {
            return new AnonymousComparer<ItemToSort>((x, y) => accessor(x).CompareTo(accessor(y)));
        }

        private IComparer<ItemToSort> create_reverse_comparer<PropertyType>(PropertyAccessor<ItemToSort, PropertyType> accessor)
            where PropertyType : IComparable<PropertyType>
        {
            return new ReverseComparer<ItemToSort>(this.create_anonymous_comparer(accessor));
        }
    }
}