using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.Core.Interfaces
{
    public interface IMathHelper
    {
        Random RandomFactory { get; set; }
        double EuclideanDistance(double[] p1, double[] p2);
        int RandomInt();
        int RandomInt(int end);
        int RandomInt(int start, int end);
        double RandomDouble();
        
    }
}
