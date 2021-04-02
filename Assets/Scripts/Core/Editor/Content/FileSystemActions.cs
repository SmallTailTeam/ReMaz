﻿using System.IO;
using ReMaz.Core.ContentContainers;
using UnityEditor;
using UnityEngine;

namespace Core.Editor.Content
{
    public static class FileSystemActions
    {
        [MenuItem("ReMaz!/Content/Delete all patterns")]
        public static void DeleteAllPatterns()
        {
            ClearDirectory(ContentFileSystem.PatternsPath);
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
                Directory.Delete(directory.FullName);
                count++;
            }
            
            Debug.Log($"Deleted {count} in {path}");
        }
    }
}