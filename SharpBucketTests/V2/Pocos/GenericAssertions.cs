namespace SharpBucketTests.V2.Pocos
{
    public static class GenericAssertions
    {
        /// <summary>
        /// Method that do nothing expect allowing to produce more expressive fluent assertions.
        /// </summary>
        public static TPoco And<TPoco>(this TPoco poco)
        {
            return poco;
        }
    }
}
