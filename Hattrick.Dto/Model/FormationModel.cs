using System;
using System.Collections.Generic;

namespace Hattrick.Manager.Model
{
    public class FormationModel : ICloneable
    {
        public PlayerInPositionModel Arquero { get; set; }
        public List<PlayerInPositionModel> Defense { get; set; }
        public List<PlayerInPositionModel> Middfield { get; set; }
        public List<PlayerInPositionModel> Side { get; set; }
        public List<PlayerInPositionModel> Forward { get; set; }
        public double TeamValue;

        public bool IsComplete
        {
            get
            {
                if (this.Arquero != null && (this.Defense.Count + this.Side.Count + this.Middfield.Count + this.Forward.Count) == 10)
                {
                    return true;
                }
                return false;
            }
        }

        public FormationModel()
        {
            this.Defense = new List<PlayerInPositionModel>();
            this.Middfield = new List<PlayerInPositionModel>();
            this.Side = new List<PlayerInPositionModel>();
            this.Forward = new List<PlayerInPositionModel>();
            this.TeamValue = 0;
        }

        public double UpdateTeamValue()
        {
            this.TeamValue = 0;
            TeamValue += Arquero.Position.Value;
            Defense.ForEach(d => TeamValue += d.Position.Value);
            Middfield.ForEach(d => TeamValue += d.Position.Value);
            Side.ForEach(d => TeamValue += d.Position.Value);
            Forward.ForEach(d => TeamValue += d.Position.Value);
            return TeamValue;
        }

        public void AddPlayer(PlayerInPositionModel playerInPositionModel)
        {
            if (playerInPositionModel.IsGoalKeeper())
            {
                if (this.Arquero == null)
                {
                    this.Arquero = playerInPositionModel;
                }
            }

            if (playerInPositionModel.IsDefense())
            {
                this.Defense.Add(playerInPositionModel);
            }

            if (playerInPositionModel.IsSide())
            {
                this.Side.Add(playerInPositionModel);
            }

            if (playerInPositionModel.IsMiddfield())
            {
                this.Middfield.Add(playerInPositionModel);
            }

            if (playerInPositionModel.IsForward())
            {
                this.Forward.Add(playerInPositionModel);
            }

            if (this.IsComplete)
            {
                this.UpdateTeamValue();
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
