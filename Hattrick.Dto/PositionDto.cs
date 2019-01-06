using Hattrick.Manager.Model;
using System.Linq;

namespace Hattrick.Dto
{
    public class PositionDto
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public PositionDto(string textPosition)
        {
            this.GetNameAndValue(textPosition);
        }

        public PositionDto()
        {

        }

        private void GetNameAndValue(string textPosition)
        {
            if (string.IsNullOrEmpty(textPosition))
            {
                return;
            }

            foreach (var field in PositionModel.Values)
            {
                if (textPosition.Contains(field))
                {
                    string[] tokens = textPosition.Split(field);
                    if (tokens.Length > 1)
                    {
                        this.Name = field;
                        this.Value = double.Parse(tokens[1].Trim());

                        if (field.Equals("Delantero Defensivo Técnico") && this.Value != 0)
                        {
                            this.Name = "Delantero Defensivo";
                        }

                        break;
                    }
                }
            }
        }
    }

    
}