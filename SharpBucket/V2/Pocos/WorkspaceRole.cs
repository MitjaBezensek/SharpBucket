namespace SharpBucket.V2.Pocos
{
    using System;

    /// <summary>
    /// Enumeration of the possible roles (or permission level) that a user can have on a workspace.
    /// </summary>
    public enum WorkspaceRole
    {
        /// <summary>
        /// User has write access to at least one repository in the workspace.
        /// </summary>
        [Obsolete("Deprecation notice - Removing Collaborator role in Bitbucket Cloud API on 1 June 2022. See https://developer.atlassian.com/cloud/bitbucket/deprecation-notice-collaborator-role/")]
        Collaborator,

        /// <summary>
        /// User is a member of at least one workspace group or repository.
        /// </summary>
        Member,

        /// <summary>
        /// User has administrator access to the workspace.
        /// </summary>
        Owner,
    }
}
