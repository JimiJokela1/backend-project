using System;
using System.ComponentModel.DataAnnotations;

namespace backend_project
{
    public class NewGame
    {
        public Guid Player_1_ID {get; set;}
        [Range(0, 1000)]
        public int Player_1_Score{get; set;}

        public Guid Player_2_ID {get; set;}
        [Range(0, 1000)]
        public int Player_2_Score{get; set;}
    }
}