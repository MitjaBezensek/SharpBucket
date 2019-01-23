using System;
using System.Collections.Generic;

namespace SharpBucket.V2.Pocos
{
    /// <summary>
    /// A tree entry could be either a file or a dictionary, and the fields that are filled depends on the effective type of each entry.
    /// </summary>
    public class TreeEntry
    {
        public const string DIRECTORY_TYPE = "commit_directory";
        public const string FILE_TYPE = "commit_file";

        public string type { get; set; }
        public string path { get; set; }
        public int? size { get; set; }
        public string mimetype { get; set; }
        public List<string> attributes { get; set; }
        public TreeEntryLinks links { get; set; }
        public CommitInfo commit { get; set; }

        /// <summary>
        /// Convert this <see cref="TreeEntry"/> into a <see cref="SrcEntry"/> to be able to read it in a more strongly typed manner.
        /// </summary>
        public SrcEntry ToSrcEntry()
        {
            switch (type)
            {
                case DIRECTORY_TYPE:
                    return new SrcEntry(ToSrcDirectory());
                case FILE_TYPE:
                    return new SrcEntry(ToSrcFile());
                default:
                    var typeValue = type != null ? $"\"{type}\"" : "null";
                    throw new InvalidOperationException($"invalid type value:{typeValue}");
            }
        }

        /// <summary>
        /// Convert this <see cref="TreeEntry"/> into a <see cref="SrcDirectory"/> or a <see cref="SrcFile"/>.
        /// </summary>
        internal ISrc ToSrc()
        {
            switch (type)
            {
                case DIRECTORY_TYPE:
                    return ToSrcDirectory();
                case FILE_TYPE:
                    return ToSrcFile();
                default:
                    var typeValue = type != null ? $"\"{type}\"" : "null";
                    throw new InvalidOperationException($"invalid type value:{typeValue}");
            }
        }

        private SrcDirectory ToSrcDirectory()
        {
            return new SrcDirectory
            {
                path = this.path,
                commit = this.commit,
                links = new SrcDirectoryLinks
                {
                    self = this.links.self,
                    meta = this.links.meta
                },
            };
        }

        private SrcFile ToSrcFile()
        {
            return new SrcFile
            {
                path = this.path,
                commit = this.commit,
                links = new SrcFileLinks
                {
                    self = this.links.self,
                    meta = this.links.meta,
                    history = this.links.history
                },
                size = this.size.GetValueOrDefault(),
                attributes = this.attributes,
                mimetype = this.mimetype
            };
        }
    }
}
