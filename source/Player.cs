using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend_project
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int HighestScore { get; set; }
        public int Mmr { get; set; }

        [DateValidation]
        public DateTime CreationTime { get; set; }

        public void Modify(ModifiedPlayer player)
        {
            if (player.HighestScore > HighestScore)
            {
                HighestScore = player.HighestScore;
            }
            Mmr = player.Mmr;
        }
    }
}
