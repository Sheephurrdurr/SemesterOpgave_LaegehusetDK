using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.DTOs
{
    public record BookConsultationResponse
    {
        public Guid ConsultationId { get; set; }
        public string? Message { get; set; }
    }
}
