namespace ReMaz.Content.Songs.Reading
{
    public interface ISongReader
    {
        SongPCM Open(string path);
        void Free();
    }
}