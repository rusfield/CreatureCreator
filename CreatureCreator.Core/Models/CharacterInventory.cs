using CreatureCreator.Core.Enums;
using CreatureCreator.Core.Models.Interfaces;


namespace CreatureCreator.Core.Models
{
    public class CharacterInventory : ICharactersSchema
    {
        public int Guid { get; set; }
        public CharacterInventorySlots Slot { get; set; }
        public int Item { get; set; }
    }
}
