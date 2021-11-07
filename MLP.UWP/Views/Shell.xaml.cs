using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MLP.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Page
    {
        public Shell()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.ContentFrame.Navigate(typeof(ModelsPage));
        }

        private void NavigateToModels(object sender, PointerRoutedEventArgs e)
        {
            this.ContentFrame.Navigate(typeof(ModelsPage));
        }

        private void NavigateToSandbox(object sender, PointerRoutedEventArgs e)
        {
            this.ContentFrame.Navigate(typeof(SandboxPage));
        }

        private void NavigationView_BackRequested(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs args)
        {
            this.ContentFrame.GoBack();
        }

        private async void DisplaySettingsDialog()
        {
            ContentDialog settingsDialog = new ContentDialog()
            {
                Content = new SettingsDialog(),
                CloseButtonText = "Close"
            };


            await settingsDialog.ShowAsync();
        }

        private void NavigationView_ItemInvoked(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                this.DisplaySettingsDialog();
            }
        }
    }
}
