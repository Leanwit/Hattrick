using System;
using System.ComponentModel.DataAnnotations.Schema;
using Hattrick.Model;

namespace Hattrick.Model
{
    public class Position : Entity
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }

        public long PlayerId { get; set; }
        [ForeignKey("PlayerId")] public virtual Player Player { get; set; }
    }
}