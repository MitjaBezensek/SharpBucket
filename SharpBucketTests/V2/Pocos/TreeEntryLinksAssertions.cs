﻿using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class TreeEntryLinksAssertions
    {
        public static TreeEntryLinks ShouldBeFilled(this TreeEntryLinks treeEntryLinks)
        {
            treeEntryLinks.ShouldNotBeNull();
            treeEntryLinks.self.ShouldBeFilled();
            treeEntryLinks.meta.ShouldBeFilled();

            return treeEntryLinks;
        }
    }
}
