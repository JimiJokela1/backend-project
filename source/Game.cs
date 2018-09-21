using System;

namespace backend_project
{
    public class Game
    {
        public Guid Id { get; set; }

        public Player Player_1 { get; set; }
        public int Player_1_Score { get; set; }
        public int Player_1_Rank_Change { get; set; }

        public Player Player_2 { get; set; }
        public int Player_2_Score { get; set; }
        public int Player_2_Rank_Change { get; set; }

        [DateValidation]
        public DateTime CreationTime { get; set; }
    }
}
