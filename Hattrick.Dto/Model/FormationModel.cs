using System.Collections.Generic;

namespace Hattrick.Manager.Model
{
    public class FormationModel
    {
        public PlayerInPositionModel Arquero { get; set; }
        public List<PlayerInPositionModel> Defensa { get; set; }
        public List<PlayerInPositionModel> Mediocampista { get; set; }
        public List<PlayerInPositionModel> Lateral { get; set; }
        public List<PlayerInPositionModel> Delanteros { get; set; }
        public double TeamValue;

        public FormationModel()
        {
            this.Defensa = new List<PlayerInPositionModel>();
            this.Mediocampista = new List<PlayerInPositionModel>();
            this.Lateral = new List<PlayerInPositionModel>();
            this.Delanteros = new List<PlayerInPositionModel>();
        }

        public double UpdateTeamValue()
        {
            this.TeamValue = 0;
            TeamValue += Arquero.Position.Value;
            Defensa.ForEach(d => TeamValue += d.Position.Value);
            Mediocampista.ForEach(d => TeamValue += d.Position.Value);
            Lateral.ForEach(d => TeamValue += d.Position.Value);
            Delanteros.ForEach(d => TeamValue += d.Position.Value);
            return TeamValue;
        }
    }
}
