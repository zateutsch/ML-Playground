using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Messaging;
using MLP.Core.Messages;

namespace MLP.Core.Services
{
    public class GraphPaletteFactory : ObservableRecipient, IRecipient<ParameterChangedMessage>
    {
        private const string _defaultPaletteColor = "Grey";

        public void Receive(ParameterChangedMessage message)
        {
            
        }
    }
}
