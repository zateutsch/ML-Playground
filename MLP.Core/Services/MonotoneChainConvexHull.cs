// see https://en.m.wikibooks.org/wiki/Algorithm_Implementation/Geometry/Convex_hull/Monotone_chain for reference on Monotone Chain algorithm

using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Common;
using MLP.Core.Interfaces;

namespace MLP.Core.Services
{
    public class MonotoneChainConvexHull : IConvexHull
    {
        private double CrossProduct(Point origin, Point a, Point b)
        {
            return ((a.X - origin.X) * (b.Y - origin.Y)) - ((a.Y - origin.Y) * (b.X - origin.X));
        }

        // translated from C++ found at https://en.m.wikibooks.org/wiki/Algorithm_Implementation/Geometry/Convex_hull/Monotone_chain#C++
        public Point[] GetConvexHull(Point[] cluster)
        {
            int numPoints = cluster.Length;
            if (numPoints <= 3) return cluster;

            Array.Sort(cluster);
            int k = 0;
            Point[] hull = new Point[2 * numPoints];

            for(int i = 0; i < numPoints; ++i)
            {
                while (k >= 2 & CrossProduct( hull[k - 2], hull[k - 1], cluster[i]) <= 0) k--;
                hull[k++] = cluster[i];
            }

            for(int i = numPoints - 1, t = k+1; i > 0; --i)
            {
                while (k >= t && CrossProduct(hull[k - 2], hull[k - 1], cluster[i - 1]) <= 0) k--;
                hull[k++] = cluster[i - 1];
            }

            Array.Resize(ref cluster, k - 1);

            return cluster;
        }
    }
}
