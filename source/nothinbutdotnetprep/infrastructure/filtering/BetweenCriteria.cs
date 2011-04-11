using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nothinbutdotnetprep.infrastructure.filtering
{
    public class BetweenCriteria<ItemToMatch, PropertyType> : Criteria<ItemToMatch> where PropertyType : IComparable<PropertyType>
    {
        private readonly ComparablePropertyAccessor<ItemToMatch, PropertyType> accessor;

        private PropertyType start;

        private PropertyType end;

        public BetweenCriteria(ComparablePropertyAccessor<ItemToMatch, PropertyType> accessor, PropertyType start, PropertyType end)
        {
            this.accessor = accessor;
            this.start = start;
            this.end = end;
        }

        public bool matches(ItemToMatch item)
        {
            var property = accessor(item);
            return property.CompareTo(start) >= 0 && property.CompareTo(end) <= 0;
        }
    }
}
