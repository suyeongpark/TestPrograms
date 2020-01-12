using System;
using System.IO;
using Test.Docker.Variable;

namespace Test.Docker.Server.MainServer
{
    public static class FileManager
    {
        static string _waitingDir;
        static string _workingDir;
        static string _fileDir;
        static string _imageDir;
        static string _thumbnailDir;

        public static void Init(string vaultDrive)
        {
            _waitingDir = vaultDrive + Pathes.VAULT_WAITING;
            _workingDir = vaultDrive + Pathes.VAULT_WORKING;
            _fileDir = vaultDrive + Pathes.VAULT_FILES;
            _imageDir = vaultDrive + Pathes.VAULT_IMAGES;
            _thumbnailDir = vaultDrive + Pathes.VAULT_THUMBNAIL;
        }

        public static void SetFileToWaitingPath(string fileName, byte[] fileData)
        {
            if (!Directory.Exists(_waitingDir))
            {
                Directory.CreateDirectory(_waitingDir);
            }

            string filePath = $"{_waitingDir}{fileName}";
            File.WriteAllBytes(path: filePath, bytes: fileData);
        }
    }
}
