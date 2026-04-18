using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.DTOs
{
    public record ChangeConsultationTypeRequest
    {
        public Guid ConsultationId { get; set; }
        public Guid ConsultationTypeId { get; set; }
    }
}
