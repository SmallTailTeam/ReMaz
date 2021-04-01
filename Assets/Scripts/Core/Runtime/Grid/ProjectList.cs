using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SmallTail.Preload.Attributes;
using UnityEngine;

namespace ReMaz.Core.Grid
{
    [Preloaded]
    public class ProjectList : MonoBehaviour
    {
        public static List<Project> Projects = new List<Project>();

        private void Awake()
        {
            LoadProjects();
        }

        private void LoadProjects()
        {
            if (!Directory.Exists("Patterns"))
            {
                return;
            }

            DirectoryInfo directory = new DirectoryInfo("Patterns");
            
            foreach (DirectoryInfo projectDirectory in directory.GetDirectories())
            {
                try
                {
                    string projectFile = projectDirectory.FullName + @"\Project.pats";
                    string json = File.ReadAllText(projectFile);

                    Project project = JsonConvert.DeserializeObject<Project>(json);
                    Projects.Add(project);
                }
                catch
                {
                    // ignore
                }
            }
        }
    }
}