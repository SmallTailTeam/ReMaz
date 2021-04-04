using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SmallTail.Preload.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ReMaz.Core.ContentContainers.Projects
{
    [Preloaded]
    public class ProjectList : MonoBehaviour, IContentContainer<Project>
    {
        private List<Project> _projects = new List<Project>();

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

                    Project project = JsonConvert.DeserializeObject<Project>(json);
                    _projects.Add(project);
                }
                catch (Exception e)
                {
					Debug.Log(e, this);
                    // ignore
                }
            }
        }

        public void Add(Project content)
        {
            _projects.Add(content);
        }

        public Project GetRandom()
        {
            Project project = _projects[Random.Range(0, _projects.Count)];
            return project;
        }

        public IList<Project> GetAll()
        {
            return _projects;
        }
    }
}