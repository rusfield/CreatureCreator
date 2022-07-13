﻿using CreatureCreator.Core.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CreatureCreator.Core.Models

{
    public class CharacterCustomizations : ICharactersSchema
    {
        public int Guid { get; set; }
        public int ChrCustomizationOptionId { get; set; }
        public int ChrCustomizationChoiceId { get; set; }
    }
}
