using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.DTOs
{
    public class BookConsultationResponse
    {
        public Guid ConsultationId { get; set; }
        public string? Message { get; set; }
    }
}
