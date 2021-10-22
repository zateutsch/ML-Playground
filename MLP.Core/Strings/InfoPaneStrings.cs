using MLP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLP.Core.Strings
{
    public static class InfoPaneStrings
    {
        public static List<InfoItem> KNNInfo = new List<InfoItem>(new[]
        {
            new InfoItem("Model Overview", "Nearest Neighbors is one of the most intuitive models out there. It solves a “classification problem” by comparing test data to its most similar neighboring data. The logic is simple: if a data point is near other data points, they might just be classified into the same category!"),
            new InfoItem("Adjusting the Model", "The main parameter you can play with for K-Nearest Neighbors is the “K” itself. K represents how many neighbors we compare to when classifying a point. If K is 3, we look at the 3 most similar points to our data."),
            new InfoItem("Algorithm", "K-Nearest Neighbors uses a “try everything” approach when it comes to picking a label. Every test data point is compared to every other data point in the test set, until the K closest neighbors are found! ")
           
        });
    }
}
