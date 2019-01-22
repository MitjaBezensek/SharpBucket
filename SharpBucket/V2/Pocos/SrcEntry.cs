using System;

namespace SharpBucket.V2.Pocos
{
    /// <summary>
    /// Class that represent a source entry.
    /// A source entry is a strongly typed representation of a <see cref="TreeEntry"/>
    /// that could either wrap a <see cref="SrcFile"/> or a <see cref="SrcDirectory"/>.
    /// It allow to provide a more friendly API to read Src results,
    /// in particular because it provide only access to the properties that are meaningful
    /// in the context of a file or a directory.
    /// </summary>
    public class SrcEntry : ISrc
    {
        private ISrc Src { get; }

        public string path => Src.path;
        public CommitInfo commit => Src.commit;
        public bool IsFile => SrcFile != null;
        public bool IsDirectory => SrcDirectory != null;
        public SrcFile SrcFile { get; }
        public SrcDirectory SrcDirectory { get; }

        public SrcEntry(SrcFile srcFile)
        {
            Src = srcFile;
            SrcFile = srcFile;
        }

        public SrcEntry(SrcDirectory srcDirectory)
        {
            Src = srcDirectory;
            SrcDirectory = srcDirectory;
        }
    }
}
