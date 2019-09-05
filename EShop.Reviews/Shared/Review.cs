namespace EShop.Reviews.Shared
{
    public class Review
    {
        public Review(int numberOfStarts, string description = null)
        {
            NumberOfStarts = GetSafeNumberOfStarts(numberOfStarts);
            Description = description ?? string.Empty;
        }

        public int NumberOfStarts { get; }

        public string Description { get; }

        private int GetSafeNumberOfStarts(int value)
        {
            const int minNumberOfStarts = 0;
            const int maxNumberOfStarts = 5;

            if (value < minNumberOfStarts)
            {
                return minNumberOfStarts;
            }

            if (value > maxNumberOfStarts)
            {
                return maxNumberOfStarts;
            }

            return value;
        }
    }
}
