using System;

namespace SharpBucket.V1.Pocos
{
    [Obsolete("Bitbucket Cloud v1 APIs are deprecated")]
    public class RepositoryPrivileges : RepositoryPrivilegesUser
    {
        public RepositorySimple repository { get; set; }
    }
}