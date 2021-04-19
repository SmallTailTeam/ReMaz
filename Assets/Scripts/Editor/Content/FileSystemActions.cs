using System.IO;
using ReMaz.Content;
using UnityEditor;
using UnityEngine;

namespace ReMaz.Editor.Content
{
    public static class FileSystemActions
    {
        [MenuItem("ReMaz!/Content/Delete all maps")]
        public static void DeleteAllPatterns()
        {
            ClearDirectory(ContentFileSystem.MapsPath);
        }
        
        [MenuItem("ReMaz!/Content/Delete all songs")]
        public static void DeleteAllSongs()
        {
            ClearDirectory(ContentFileSystem.SongsPath);
        }

        private static void ClearDirectory(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);

            int count = 0;

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                File.Delete(fileInfo.FullName);
                count++;
            }
            
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                if (Directory.GetFiles(directory.FullName).Length < 1)
                {
                    Directory.Delete(directory.FullName);
                }
                else
                {
                    ClearDirectory(directory.FullName);
                    Directory.Delete(directory.FullName);
                }

                count++;
            }
            
            Debug.Log($"Deleted {count} in {path}");
        }
    }
}