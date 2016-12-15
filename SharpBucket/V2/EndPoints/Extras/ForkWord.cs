namespace SharpBucket.V2.EndPoints.Extras
{
    public class ForkWord : ApiWord
    {
        public static readonly ForkWord AllowForks = new ForkWord() {Name = "allow_forks"};
        public static readonly ForkWord NoPublicForks = new ForkWord() { Name = "no_public_forks" };
        public static readonly ForkWord NoForks = new ForkWord() { Name = "no_forks" };
    }
}