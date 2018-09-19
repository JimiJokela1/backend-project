using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend_project
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int Mmr { get; set; }

        [DateValidation]
        public DateTime CreationTime { get; set; }

        public void Modify(ModifiedPlayer player)
        {
            Score = player.Score;
        }
    }
}
