using System;
using System.Collections.Generic;

namespace Test.Docker.Type
{
    [Serializable]
    public struct FileInfo
    {
        public FileInfo(uint id, string fileName, string fileType)
        {
            this.ID = id;
            this.FileName = fileName;
            this.FileType = fileType;
        }

        public uint ID { get; private set; }
        public string FileName { get; private set; }
        public string FileType { get; private set; }
    }

    [Serializable]
    public class FileInfoCollection : List<FileInfo>
    {
        public FileInfoCollection()
        {

        }
    }
}
