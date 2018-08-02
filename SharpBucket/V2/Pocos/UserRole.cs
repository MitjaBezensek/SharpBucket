namespace SharpBucket.V2.Pocos
{
    public class UserRole
    {
        public User User { get; set; }
        public string Role { get; set; }
        public bool Approved { get; set; }
    }
}