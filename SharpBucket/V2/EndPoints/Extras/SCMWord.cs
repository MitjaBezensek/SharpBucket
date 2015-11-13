namespace SharpBucket.V2.EndPoints.Extras
{
    public class SCMWord : ApiWord
    {
        public static readonly SCMWord Git = new SCMWord() { Name = "git" };
        public static readonly SCMWord Hg = new SCMWord() { Name = "hg" };
    }
}