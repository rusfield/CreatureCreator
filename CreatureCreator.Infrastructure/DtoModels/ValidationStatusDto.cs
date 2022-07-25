using CreatureCreator.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreatureCreator.Infrastructure.DtoModels
{
    public class ValidationStatusDto
    {
        public SystemValidations Name { get; set; }
        public ValidationStatuses Status { get; set; }
        public string Description { get; set; }
    }
}
