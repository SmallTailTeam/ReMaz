using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ReMaz.Core.Content;
using ReMaz.Core.Content.Projects;
using ReMaz.Core.Content.Projects.Patterns;
using UniRx;
using UnityEngine;

namespace ReMaz.PatternEditor
{
    public class PatternProjectEditor : MonoBehaviour, IProjectEditor<IProject<Pattern>>
    {
        public IObservable<IProject<Pattern>> ProjectLoaded => _projectLoaded;
        
        public IProject<Pattern> Project { get; private set; }

        private ISubject<IProject<Pattern>> _projectLoaded = new Subject<IProject<Pattern>>();

        public void Open(IProject<Pattern> project)
        {
            Project = project;
            
            _projectLoaded?.OnNext(Project);
        }

        public void Create()
        {
            Project = new ProjectPattern(Guid.NewGuid().ToString(), "Unnamed");
            
            _projectLoaded?.OnNext(Project);
        }

        public void Save()
        {
            Project.Content.BoundLeft = Project.Content.Tiles.Min(tile => tile.Position.x);
            Project.Content.BoundRight = Project.Content.Tiles.Max(tile => tile.Position.x);
            
            string json = JsonConvert.SerializeObject(Project);
            
            if (!Directory.Exists(ContentFileSystem.PatternsPath))
            {
                Directory.CreateDirectory(ContentFileSystem.PatternsPath);
            }

            if (!Directory.Exists($"{ContentFileSystem.PatternsPath}/{Project.Id}"))
            {
                Directory.CreateDirectory($"{ContentFileSystem.PatternsPath}/{Project.Id}");
            }

            File.AppendAllText($"{ContentFileSystem.PatternsPath}/{Project.Id}/Project.pats", json);
        }

        private void Start()
        {
            if (Project == null)
            {
                Create();
            }
        }
    }
}