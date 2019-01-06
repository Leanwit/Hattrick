using Hattrick.Dto;

namespace Hattrick.Manager.Model
{
    public class PlayerInPositionModel
    {
        public string Name;
        public PositionDto Position;

        public PlayerInPositionModel()
        {

        }

        public PlayerInPositionModel(string name, PositionDto position)
        {
            Name = name;
            Position = position;
        }
    }


}
