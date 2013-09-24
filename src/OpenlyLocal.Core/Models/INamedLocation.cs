using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.Models
{
    public interface INamedLocation : ILocation
    {
        string Name { get; }
    }
}
