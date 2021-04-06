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