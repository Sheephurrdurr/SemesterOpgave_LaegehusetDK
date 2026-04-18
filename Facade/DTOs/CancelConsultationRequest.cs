using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.DTOs
{
    public record CancelConsultationRequest
    {
        public Guid Id { get; set; }
    }
}
