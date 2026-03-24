using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class FullName
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

    }
}
