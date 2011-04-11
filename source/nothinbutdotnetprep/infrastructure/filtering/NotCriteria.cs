namespace nothinbutdotnetprep.infrastructure.filtering
{
    public class NotCriteria<ItemToMatch> : Criteria<ItemToMatch>
    {
        private readonly Criteria<ItemToMatch> original;

        public NotCriteria(Criteria<ItemToMatch> original)
        {
            this.original = original;
        }

        public bool matches(ItemToMatch item)
        {
            return !original.matches(item);
        }
    }
}