﻿using System;
using System.Collections.Generic;
using System.Text;
using MLP.Core.Interfaces;

namespace MLP.Core.Services
{
    public class MathHelper : IMathHelper
    {
        public Random RandomFactory { get; set; }

        public MathHelper()
        {
            this.RandomFactory = new Random();
        }

        public double EuclideanDistance(double[] p1, double[] p2)
        {
            double squared_differences = 0;

            for(int i = 0; i < p1.Length; i++)
            {
                squared_differences += ((p1[i] - p2[i]) * (p1[i] - p2[i]));
            }

            return Math.Sqrt(squared_differences);
        }

        public int RandomInt()
        {
            return this.RandomFactory.Next();
        }
        public int RandomInt(int end)
        {
            return this.RandomFactory.Next(end);
        }
        public int RandomInt(int start, int end)
        {
            return this.RandomFactory.Next(start, end);
        }
        public double RandomDouble()
        {
            return this.RandomFactory.NextDouble();
        }
    }
}
