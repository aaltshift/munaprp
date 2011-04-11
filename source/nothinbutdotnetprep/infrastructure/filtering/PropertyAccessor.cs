namespace nothinbutdotnetprep.infrastructure.filtering
{
    using System;

    public delegate PropertyType PropertyAccessor<in ItemWithProperty, out PropertyType>(
        ItemWithProperty item_with_property);

    public delegate PropertyType ComparablePropertyAccessor<in ItemWithProperty, out PropertyType>(
        ItemWithProperty item_with_property) where PropertyType : IComparable<PropertyType>;

}