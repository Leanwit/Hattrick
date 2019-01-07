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

        public bool IsGoalKeeper()
        {
            return this.Position.Name.Equals(PositionModel.Arquero);
        }

        public bool IsDefense()
        {
            return PositionModel.GetDefenseValues().Exists(x => x.Equals(Position.Name));
        }

        public bool IsSide()
        {
            return PositionModel.GetSideValues().Exists(x => x.Equals(Position.Name));
        }

        public bool IsMiddfield()
        {
            return PositionModel.GetMiddfieldValues().Exists(x => x.Equals(Position.Name));
        }

        public bool IsForward()
        {
            return PositionModel.GetForwardValues().Exists(x => x.Equals(Position.Name));
        }
    }


}
