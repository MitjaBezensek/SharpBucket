namespace SharpBucket.V2.Pocos
{
    public class Team : User
    {
        public new string username
        {
#pragma warning disable 618 // for GDPR concerns username has been removed for users but not for teams.
            get => base.username;
            set => base.username = value;
#pragma warning restore 618
        }
    }
}