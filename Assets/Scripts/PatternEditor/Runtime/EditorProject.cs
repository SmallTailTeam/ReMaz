using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ReMaz.Core.Grid;
using UnityEngine;

namespace ReMaz.PatternEditor
{
    public class EditorProject : MonoBehaviour
    {
        public static Project CurrentProject;

        public static void Open(Project pattern)
        {
            CurrentProject = pattern;
        }

        public static void Create()
        {
            CurrentProject = new Project
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Unnamed",
                Pattern = new Pattern()
            };
        }

        public static void Save()
        {
            CurrentProject.Pattern.BoundLeft = CurrentProject.Pattern.Tiles.Min(tile => tile.Position.x);
            CurrentProject.Pattern.BoundRight = CurrentProject.Pattern.Tiles.Max(tile => tile.Position.x);
            
            string json = JsonConvert.SerializeObject(CurrentProject);
            
            if (!Directory.Exists("Patterns"))
            {
                Directory.CreateDirectory("Patterns");
            }

            if (!Directory.Exists($"Patterns/{CurrentProject.Id}"))
            {
                Directory.CreateDirectory($"Patterns/{CurrentProject.Id}");
            }

            File.AppendAllText($"Patterns/{CurrentProject.Id}/Project.pats", json);
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