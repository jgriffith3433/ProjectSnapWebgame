using System;

namespace MHLab.ReactUI.Core
{
    [Serializable]
    public sealed class UIEvent<T> where T : UIEventPayload
    {
        public string eventName;
        public T payload;
        
        public UIEvent()
        {
            
        }

        public UIEvent(string eventName, T payload)
        {
            this.eventName = eventName;
            this.payload = payload;
        }
    }
}