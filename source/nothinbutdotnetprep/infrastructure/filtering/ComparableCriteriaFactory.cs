using System;

namespace nothinbutdotnetprep.infrastructure.filtering
{
    public class ComparableCriteriaFactory<ItemToMatch,PropertyType> where PropertyType : IComparable<PropertyType>
    {
        private ComparablePropertyAccessor<ItemToMatch, PropertyType> accessor;

        public ComparableCriteriaFactory(ComparablePropertyAccessor<ItemToMatch, PropertyType> accessor)
        {
            this.accessor = accessor;
        }

        public Criteria<ItemToMatch> between(PropertyType start, PropertyType end)
        {
            return new BetweenCriteria<ItemToMatch, PropertyType>(this.accessor, start, end);
        }
    }
}