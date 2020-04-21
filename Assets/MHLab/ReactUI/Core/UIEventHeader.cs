using System;

namespace MHLab.ReactUI.Core
{
    [Serializable]
    public struct UIEventHeader
    {
        public string eventName;
        public string payload;

        public UIEventHeader(string eventName, string payload)
        {
            this.eventName = eventName;
            this.payload = payload;
        }
    }
}