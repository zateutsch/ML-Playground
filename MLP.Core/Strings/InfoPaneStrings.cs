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
            new InfoItem("Model Overview", "K-Nearest Neighbors is one of the most intuitive models out there. It solves a “classification problem” by comparing test data to its most similar neighboring data. The logic is simple: if a data point is near other data points, they might just be classified into the same category!"),
            new InfoItem("Adjusting the Model", "The main parameter you can play with for K-Nearest Neighbors is the “K” itself. K represents how many neighbors we compare to when classifying a point. If K is 3, we look at the 3 most similar points to our data."),
            new InfoItem("Algorithm", "K-Nearest Neighbors uses a “try everything” approach when it comes to picking a label. Every test data point is compared to every other data point in the test set, until the K closest neighbors are found!"),
            new InfoItem("Strengths", "KNN really shines in its simplicity. The model is easy to implement, understand, and visualize. Even as the dimensionality of the model increases, the idea remains the same: what do the most similar points tell us about our data?")
           
        });

        public static List<InfoItem> KMeansInfo = new List<InfoItem>(new[]
        {
            new InfoItem("Model Overview", "The K-Means Clustering model is another simple model that groups data into similar groups called \"clusters\". The model works with unlabeled data, and instead of guessing a label, it tries to sort data into segmented groups."),
            new InfoItem("Adjusting the Model", "The main parameter we can work with here is the \"K\" in the model name. K represents how many different clusters we want to generate, and we can change this value to adjust how our data is seperated"),
            new InfoItem("Algorithm", "The K Means algorithm starts by picking random center points for each cluster. All of the data being clustered is then assigned to whichever \"centroid\" it is closests to. The new centroids are moved to the average of all the data points in the cluster, and the process is repeated until data stops getting reassigned."),
            new InfoItem("Strengths", "K Means is great for getting insight from data that doesn't have labels and can't be easily classified. Separating data into clusters is a great way to look for revealing patterns in a data set.")
        });
    }
}
