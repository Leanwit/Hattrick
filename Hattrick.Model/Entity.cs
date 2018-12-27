using System.ComponentModel.DataAnnotations;

namespace Hattrick.Model
{
    public class Entity
    {
        [Key]
        public long Id { get; set; }
    }
}