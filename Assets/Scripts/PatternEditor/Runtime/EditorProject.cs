using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ReMaz.Core.Content;
using ReMaz.Core.Content.Projects;
using ReMaz.Core.Content.Projects.Patterns;
using UnityEngine;

namespace ReMaz.PatternEditor
{
    public class EditorProject : MonoBehaviour
    {
        public static IProject<Pattern> CurrentProject;

        public static void Open(ProjectPattern pattern)
        {
            CurrentProject = pattern;
        }

        public static void Create()
        {
            CurrentProject = new ProjectPattern(Guid.NewGuid().ToString(), "Unnamed");
        }

        public static void Save()
        {
            CurrentProject.Content.BoundLeft = CurrentProject.Content.Tiles.Min(tile => tile.Position.x);
            CurrentProject.Content.BoundRight = CurrentProject.Content.Tiles.Max(tile => tile.Position.x);
            
            string json = JsonConvert.SerializeObject(CurrentProject);
            
            if (!Directory.Exists(ContentFileSystem.PatternsPath))
            {
                Directory.CreateDirectory(ContentFileSystem.PatternsPath);
            }

            if (!Directory.Exists($"{ContentFileSystem.PatternsPath}/{CurrentProject.Id}"))
            {
                Directory.CreateDirectory($"{ContentFileSystem.PatternsPath}/{CurrentProject.Id}");
            }

            File.AppendAllText($"{ContentFileSystem.PatternsPath}/{CurrentProject.Id}/Project.pats", json);
        }

        private void Awake()
        {
            if (CurrentProject == null)
            {
                Create();
            }
        }
    }
}