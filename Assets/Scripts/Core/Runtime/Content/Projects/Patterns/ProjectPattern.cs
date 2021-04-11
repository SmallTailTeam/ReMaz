using Newtonsoft.Json;

namespace ReMaz.Core.Content.Projects.Patterns
{
    public class ProjectPattern : IProject<Pattern>, IPublishTo<WorkshopData>
    {
        public string Id { get; }
        public string Name { get; private set; }
        public Pattern Content { get; }
        public WorkshopData PublishData { get; private set; }

        public ProjectPattern(string id, string name)
        {
            Id = id;
            Name = name;
            Content = new Pattern();
        }

        [JsonConstructor]
        public ProjectPattern(string id, string name, Pattern content, WorkshopData publishData)
        {
            Id = id;
            Name = name;
            Content = content;
            PublishData = publishData;
        }

        public void Rename(string name)
        {
            Name = name;
        }

        public void Publish(WorkshopData publishData)
        {
            PublishData = publishData;
        }
    }
}