using CreatureCreator.Core.Enums;
using CreatureCreator.Core.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CreatureCreator.Core.Models
{
    public class Characters : ICharactersSchema
    {
        [Key]
        public int Guid { get; set; }
        public string Name { get; set; }
        public Races Race { get; set; }
        public Genders Gender { get; set; }
        public int Level { get; set; }
    }
}
