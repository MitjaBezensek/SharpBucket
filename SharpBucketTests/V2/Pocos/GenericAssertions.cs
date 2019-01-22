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

        /// <summary>
        /// Method that do nothing expect allowing to express the fact that we can't do any real assertion on a data.
        /// </summary>
        public static TPoco CouldBeNull<TPoco>(this TPoco poco)
            where TPoco : class
        {
            return poco;
        }
    }
}
