namespace nothinbutdotnetprep.infrastructure.sorting
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ComparerChainer<ItemToSort> : IEnumerable<ItemToSort>
    {
        private readonly IEnumerable<ItemToSort> collection;

        private readonly ComparerBuilder<ItemToSort> comparerBuilder;

        public ComparerChainer(IEnumerable<ItemToSort> collection) : this(collection, new EmptyComparer<ItemToSort>())
        {
        }

        public ComparerChainer(IEnumerable<ItemToSort> collection, IComparer<ItemToSort> comparer)
        {
            this.collection = collection;
            this.comparerBuilder = new ComparerBuilder<ItemToSort>(comparer);
        }

        public ComparerChainer<ItemToSort> then_by<PropertyType>(PropertyAccessor<ItemToSort, PropertyType> accessor,
                                                          params PropertyType[] fixed_order)
        {
            return this.build_chainer(this.comparerBuilder.then_by(accessor, fixed_order));
        }

        public ComparerChainer<ItemToSort> then_by<PropertyType>(PropertyAccessor<ItemToSort, PropertyType> accessor)
            where PropertyType : IComparable<PropertyType>
        {
            return this.build_chainer(this.comparerBuilder.then_by(accessor));
        }

        public ComparerChainer<ItemToSort> then_by_descending<PropertyType>(
            PropertyAccessor<ItemToSort, PropertyType> accessor)
            where PropertyType : IComparable<PropertyType>
        {
            return this.build_chainer(this.comparerBuilder.then_by_descending(accessor));
        }

        private ComparerChainer<ItemToSort> build_chainer(ComparerBuilder<ItemToSort> comparerBuilder)
        {
            return new ComparerChainer<ItemToSort>(this.collection, comparerBuilder);
        }

        public IEnumerator<ItemToSort> GetEnumerator()
        {
            return collection.sort_using(comparerBuilder).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.sort_using(comparerBuilder).GetEnumerator();
        }
    }
}