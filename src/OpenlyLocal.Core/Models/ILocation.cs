using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.Models
{
    public interface ILocation
    {
        double Lat { get; }
        double Lng { get; }
    }

}
