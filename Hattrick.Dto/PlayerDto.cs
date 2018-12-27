using System.Collections.Generic;

namespace Hattrick.Dto
{
    public class PlayerDto
    {
        public string Name { get; set; }
        public List<PositionDto> Positions { get; set; }
        public string Age { get; set; }
    }
}