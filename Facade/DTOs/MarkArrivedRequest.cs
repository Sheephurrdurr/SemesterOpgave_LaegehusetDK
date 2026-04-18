using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.DTOs
{
    public record MarkArrivedRequest
    {
        public Guid Id { get; set; }
    }
}
