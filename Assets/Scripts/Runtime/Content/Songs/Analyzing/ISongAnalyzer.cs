using ReMaz.Content.Songs.Reading;

namespace ReMaz.Content.Songs.Analyzing
{
    public interface ISongAnalyzer
    {
        SongSpectrum Analyze(SongPCM pcm);
    }
}