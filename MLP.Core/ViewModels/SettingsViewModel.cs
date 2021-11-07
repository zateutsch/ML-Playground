using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using MLP.Core.Services;

namespace MLP.Core.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private string header;
        private string githubText = "This project is open source and uses publicly available data! Check out the source code for this project, along with the sources for all of the data.";
        private string githubUri = "https://github.com/zateutsch/ML-Playground";
        private string dataSourcesText = "All data source credit goes towards the original authors.";
        private string dataSourcesUri = "https://github.com/zateutsch/ML-Playground/blob/main/DataSources.md";
        private string iconCredit = "Icon created by Gacem Tachfin from the Noun Project.";
        private string logoPath = "../Assets/StoreLogoWhite.png";

        public string Header
        {
            get => header;
            set => SetProperty(ref header, value);
        }

        public string GithubText
        {
            get => githubText;
            set => SetProperty(ref githubText, value);
        }

        public string GithubUri
        {
            get => githubUri;
            set => SetProperty(ref githubUri, value);
        }

        public string DataSourcesText
        {
            get => dataSourcesText;
            set => SetProperty(ref dataSourcesText, value);
        }

        public string DataSourcesUri
        {
            get => dataSourcesUri;
            set => SetProperty(ref dataSourcesUri, value);
        }
        
        public string IconCredit
        {
            get => iconCredit;
            set => SetProperty(ref iconCredit, value);
        }

        public string LogoPath
        {
            get => logoPath;
            set => SetProperty(ref logoPath, value);
        }
    }
}
