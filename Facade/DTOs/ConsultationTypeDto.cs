using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.DTOs
{
    public record ConsultationTypeDto(
        Guid Id,
        string Name,
        TimeSpan Duration
    );
}
