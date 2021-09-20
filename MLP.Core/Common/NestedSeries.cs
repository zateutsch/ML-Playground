using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MLP.Core.Common
{

    // Nested series class for creating Observable collections of Observable collections
    // For data-binding with Telerik Series Providers for Dynamic Series Generation

    public class NestedSeries<T>
    {
        public ObservableCollection<DataPoint<T>> Data { get; set; }

        public NestedSeries(T[] x_data, T[] y_data)
        {
            this.Data = new ObservableCollection<DataPoint<T>>();

            for(int i = 0; i < x_data.Length; i++)
            {
                this.Data.Add(new DataPoint<T>(x_data[i], y_data[i]));
            }
        }

        public NestedSeries(List<DataPoint<T>> fullSeries)
        {
            this.Data = new ObservableCollection<DataPoint<T>>(fullSeries);
        }
    }
}
