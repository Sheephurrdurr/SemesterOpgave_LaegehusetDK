using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.DTOs
{
    public record PatientDto(
        Guid Id,
        string Name
    );
}
