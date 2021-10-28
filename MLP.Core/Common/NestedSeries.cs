using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MLP.Core.Common
{

    // Nested series class for creating Observable collections of Observable collections
    // For data-binding with Telerik Series Providers for Dynamic Series Generation

    public class NestedSeries
    {
        public ObservableCollection<Point> Data { get; set; }

        public NestedSeries(double[] x_data, double[] y_data)
        {
            this.Data = new ObservableCollection<Point>();

            for(int i = 0; i < x_data.Length; i++)
            {
                this.Data.Add(new Point(x_data[i], y_data[i]));
            }
        }

        public NestedSeries(List<Point> fullSeries)
        {
            this.Data = new ObservableCollection<Point>(fullSeries);
        }
    }
}
