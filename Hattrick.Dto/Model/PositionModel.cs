using System;
using System.Collections.Generic;
using System.Text;

namespace Hattrick.Manager.Model
{
    public class PositionModel
    {
        public static string[] Values =
        {
            "Defensa Central ofensivo", "Defensa Lateral hacia medio", "Defensa Central hacia el lateral",
            "Defensa Central", "Defensa Lateral ofensivo", "Defensa Lateral defensivo", "Defensa Lateral",
            "Lateral Defensivo", "Lateral hacia medio", "Lateral Ofensivo", "Lateral", "Mediocampista Ofensivo",
            "Mediocampista Defensivo", "Mediocampista hacia el lateral", "Mediocampista", "Delantero Defensivo Técnico",
            "Delantero Defensivo", "Defensa Lateral defensivo", "Delantero hacia el lateral", "Delantero", "Arquero"
        };

        public static string Arquero = "Arquero";
        public static string DefensaCentralofensivo = "Defensa Central ofensivo";
        public static string DefensaCentral = "Defensa Central";
        public static string DefensaCentralhaciaellateral = "Defensa Central hacia el lateral";
        public static string DefensaLateralhaciamedio = "Defensa Lateral hacia medio";
        public static string DefensaLateralofensivo = "Defensa Lateral ofensivo";
        public static string DefensaLateral = "Defensa Lateral";
        public static string DefensaLateraldefensivo = "Defensa Lateral defensivo";
        public static string MediocampistaOfensivo = "Mediocampista Ofensivo";
        public static string Mediocampista = "Mediocampista";
        public static string MediocampistaDefensivo = "Mediocampista Defensivo";
        public static string Mediocampistahaciaellateral = "Mediocampista hacia el lateral";
        public static string Lateral = "Lateral";
        public static string Lateralhaciamedio = "Lateral hacia medio";
        public static string LateralDefensivo = "Lateral Defensivo";
        public static string LateralOfensivo = "Lateral Ofensivo";
        public static string Delantero = "Delantero";
        public static string Delanterohaciaellateral = "Delantero hacia el lateral";
        public static string DelanteroDefensivoTécnico = "Delantero Defensivo Técnico";
        public static string DelanteroDefensiv = "Delantero Defensivo";

        public static List<string> GetDefenseValues()
        {
            return new List<string>() {
                DefensaCentralofensivo,
                DefensaCentral,
                DefensaCentralhaciaellateral,
                DefensaLateralofensivo,
                DefensaLateral,
                DefensaLateraldefensivo
            };
        }
    }
}
