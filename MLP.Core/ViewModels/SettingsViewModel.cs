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
        private string githubText;
        private string githubUri;
        private string dataSourcesText;
        private string dataSourcesUri;
        private string iconCredit = "Icon created by Gacem Tachfin from the Noun Project.";
        private string logoPath;

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
