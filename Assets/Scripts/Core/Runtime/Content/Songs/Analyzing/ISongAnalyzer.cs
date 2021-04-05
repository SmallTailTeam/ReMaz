using ReMaz.Core.Content.Songs.Reading;

namespace ReMaz.Core.Content.Songs.Analyzing
{
    public interface ISongAnalyzer
    {
        SongSpectrum Analyze(SongPCM pcm);
    }
}