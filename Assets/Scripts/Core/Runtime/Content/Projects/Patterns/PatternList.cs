using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SmallTail.Preload.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ReMaz.Core.Content.Projects.Patterns
{
    [Preloaded]
    public class PatternList : MonoBehaviour, IContentContainer<ProjectPattern>
    {
        public IObservable<ProjectPattern> Added { get; }
        public bool HasContent { get; }

        private List<ProjectPattern> _projects = new List<ProjectPattern>();

        private void Awake()
        {
            LoadProjects();
        }

        private void LoadProjects()
        {
            if (!Directory.Exists(ContentFileSystem.PatternsPath))
            {
                return;
            }

            DirectoryInfo directory = new DirectoryInfo(ContentFileSystem.PatternsPath);
            
            foreach (DirectoryInfo projectDirectory in directory.GetDirectories())
            {
                try
                {
                    string projectFile = projectDirectory.FullName + @"\Project.pats";
                    string json = File.ReadAllText(projectFile);

                    ProjectPattern projectPattern = JsonConvert.DeserializeObject<ProjectPattern>(json);

                    _projects.Add(projectPattern);
                }
                catch (Exception e)
                {
					Debug.Log(e, this);
                    // ignore
                }
            }
        }

        public void Add(ProjectPattern content)
        {
            _projects.Add(content);
        }

        public ProjectPattern GetRandom()
        {
            ProjectPattern projectPattern = _projects[Random.Range(0, _projects.Count)];
            return projectPattern;
        }

        public IList<ProjectPattern> GetAll()
        {
            return _projects;
        }
    }
}