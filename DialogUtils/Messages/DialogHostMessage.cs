using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DialogUtils.Messages
{
    public class DialogHostMessage
    {
        public DialogHostMessage(string dialogIdentifier, DialogHostEvent dialogHostEvent)
        {
            DialogIdentifier = dialogIdentifier;
            DialogHostEvent = dialogHostEvent;
        }

        public string DialogIdentifier { get; private set; }
        public DialogHostEvent DialogHostEvent { get; private set; }
    }

    public enum DialogHostEvent
    {
        Opened,
        Closing
    }
}
