using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ReMaz.Content;
using SmallTail.Preload.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ReMaz.Grid.Maps
{
    public class Maps : MonoBehaviour, IContentContainer<Map>
    {
        public IObservable<Map> Added { get; }
        public bool HasContent { get; }

        private List<Map> _maps = new List<Map>();

        private void Awake()
        {
            LoadProjects();
        }

        private void LoadProjects()
        {
            if (!Directory.Exists(ContentFileSystem.MapsPath))
            {
                return;
            }

            DirectoryInfo directory = new DirectoryInfo(ContentFileSystem.MapsPath);
            
            foreach (DirectoryInfo projectDirectory in directory.GetDirectories())
            {
                try
                {
                    string projectFile = projectDirectory.FullName + @"\Project.pats";
                    string json = File.ReadAllText(projectFile);

                    Map map = JsonConvert.DeserializeObject<Map>(json);

                    _maps.Add(map);
                }
                catch (Exception e)
                {
					Debug.Log(e, this);
                    // ignore
                }
            }
        }

        public void Add(Map content)
        {
            _maps.Add(content);
        }

        public Map GetRandom()
        {
            Map map = _maps[Random.Range(0, _maps.Count)];
            return map;
        }

        public IList<Map> GetAll()
        {
            return _maps;
        }
    }
}