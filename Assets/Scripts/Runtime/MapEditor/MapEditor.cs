using System;
using System.IO;
using Newtonsoft.Json;
using ReMaz.Content;
using ReMaz.Grid.Maps;
using UniRx;
using UnityEngine;

namespace ReMaz.MapEditor
{
    public class MapEditor : MonoBehaviour, IEditor<Map>
    {
        public IObservable<Map> ProjectLoaded => _projectLoaded;
        
        public Map Project { get; private set; }

        private ISubject<Map> _projectLoaded = new Subject<Map>();

        public void Open(Map project)
        {
            Project = project;
            
            _projectLoaded?.OnNext(Project);
        }

        public void Create()
        {
            Project = new Map()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "A cool map"
            };
            
            _projectLoaded?.OnNext(Project);
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(Project);
            
            if (!Directory.Exists(ContentFileSystem.MapsPath))
            {
                Directory.CreateDirectory(ContentFileSystem.MapsPath);
            }

            if (!Directory.Exists($"{ContentFileSystem.MapsPath}/{Project.Id}"))
            {
                Directory.CreateDirectory($"{ContentFileSystem.MapsPath}/{Project.Id}");
            }

            File.AppendAllText($"{ContentFileSystem.MapsPath}/{Project.Id}/Project.pats", json);
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