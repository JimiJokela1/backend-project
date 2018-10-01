using System.ComponentModel.DataAnnotations;

namespace backend_project
{
    public class NewPlayer
    {
        [StringLength(32)]
        public string Name { get; set; }
    }
}
