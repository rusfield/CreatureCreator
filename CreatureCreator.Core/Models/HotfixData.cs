using CreatureCreator.Core.Enums;
using CreatureCreator.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Core.Models
{
    public class HotfixData : IHotfixesSchema
    {
        [Key]
        public int Id { get; set; }
        public int UniqueId { get; set; }
        public long TableHash { get; set; }
        public int RecordId { get; set; }
        public HotfixStatus Status { get; set; }
        public int VerifiedBuild { get; set; }

    }
}
