using System.Collections.Generic;

namespace ReMaz.Content.Songs.Analyzing
{
    public class SongSpectrum
    {
        public int Skip = 0;
        public int Take = 5;
        public int StoreEvery = 4;
        public List<float[]> Bands = new List<float[]>();
        public List<float> Averages = new List<float>();
    }
}