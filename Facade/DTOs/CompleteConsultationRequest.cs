using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.DTOs
{
    public class CompleteConsultationRequest
    {
        public Guid Id { get; set; }
        public string? Note { get; set; } // Optional note to be added when completing the consultation.
    }
}
