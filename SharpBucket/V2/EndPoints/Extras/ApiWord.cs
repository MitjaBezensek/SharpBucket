namespace SharpBucket.V2.EndPoints.Extras
{
    public abstract class ApiWord
    {
        public string Name { get; protected set; }

        protected ApiWord()
        {
        }

        protected ApiWord(string name)
        {
            Name = name;
        }
    }
}