using System;
using System.Collections.Generic;
using System.Text;

namespace MLP.MachineLearning.Services
{
    public class MathHelper : IMathHelper
    {
        public double EuclideanDistance(List<double> p1, List<double> p2)
        {
            double squared_differences = 0;

            for(int i = 0; i < p1.Count; i++)
            {
                squared_differences += ((p1[i] - p2[i]) * (p1[i] - p2[i]));
            }

            return Math.Sqrt(squared_differences);
        }
    }
}
