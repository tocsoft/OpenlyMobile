using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.Models
{
    public class GeoPolygon : List<GeoLine>
    {
    }

    public class GeoLine : List<double[]>
    {
    }
}
