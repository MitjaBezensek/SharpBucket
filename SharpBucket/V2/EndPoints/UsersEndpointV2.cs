
namespace SharpBucket.V2.EndPoints{
    public class UsersEndpointV2{
        private readonly SharpBucketV2 _sharpBucketV2;
        private readonly string _accountName;

        public UsersEndpointV2(string accountName, SharpBucketV2 sharpBucketV2){
            _accountName = accountName;
            _sharpBucketV2 = sharpBucketV2;
        }

        //public Profile GetProfile(){
        //    return _sharpBucketV2.Get(new UsersRoutes.GetProfile{AccountName = _accountName});
        //}

        //public ListOfUsers ListFollowers(){
        //    return _sharpBucketV2.Get(new UsersRoutes.ListFollowers{AccountName = _accountName});
        //}

        //public ListOfUsers ListFollowing(){
        //    return _sharpBucketV2.Get(new UsersRoutes.ListFollowing{AccountName = _accountName});
        //}

        //public List<Repository> ListRepositories(){
        //    return _sharpBucketV2.Get(new UsersRoutes.ListRepositories{AccountName = _accountName});
        //}
    }
}