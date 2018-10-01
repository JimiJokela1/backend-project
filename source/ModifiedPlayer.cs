using System.ComponentModel.DataAnnotations;

namespace backend_project
{
    public class ModifiedPlayer
    {
        [Range(0, 1000)]
        public int HighestScore { get; set; }
        public int Mmr { get; set; }
    }
}
