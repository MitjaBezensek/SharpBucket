using Newtonsoft.Json.Serialization;

namespace SharpBucket.Authentication{
    public class LowerCaseResolver : DefaultContractResolver{
        protected override string ResolvePropertyName(string propertyName){
            return propertyName.ToLower();
        }
        
    }
}