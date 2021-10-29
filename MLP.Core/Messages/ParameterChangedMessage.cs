using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;

namespace MLP.Core.Messages
{
    public class ParameterChangedMessage : ValueChangedMessage<int>
    {
        public ParameterChangedMessage(int param) : base(param)
        {
        }
    }
}
