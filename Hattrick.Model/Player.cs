using System.Collections.Generic;
using Hattrick.Model;

namespace Hattrick.Model
{
    public class Player : Entity
    {
        public string Name { get; set; }
        public List<Position> Positions { get; set; }
        public string Age { get; set; }
    }
}