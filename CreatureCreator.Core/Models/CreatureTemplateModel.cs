using CreatureCreator.Core.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CreatureCreator.Core.Models
{
    public class CreatureTemplateModel : IWorldSchema
    {
        public int CreatureId { get; set; }
        public int CreatureDisplayId { get; set; }
        public int DisplayScale { get; set; }
        public int Probability { get; set; }
        public int VerifiedBuild { get; set; }
    }
}
