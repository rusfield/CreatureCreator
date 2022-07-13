﻿using CreatureCreator.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Core.Models
{
    public class ItemModifiedAppearance : IDb2
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int ItemAppearanceModifierId { get; set; }
        public int ItemAppearanceId { get; set; }
        public int OrderIndex { get; set; }
    }
}
