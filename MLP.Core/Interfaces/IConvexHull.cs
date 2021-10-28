using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Common;

namespace MLP.Core.Interfaces
{
    public interface IConvexHull
    {
        Point[] GetConvexHull(Point[] cluster);
    }
}
