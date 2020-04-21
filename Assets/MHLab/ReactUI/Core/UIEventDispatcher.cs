using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MHLab.ReactUI.Core
{
    public class UIEventDispatcher : MonoBehaviour
    {
        public static UIEventDispatcher Instance;
        private readonly Dictionary<string, Action<string, UIEventPayload>> _eventBindings = new Dictionary<string, Action<string, UIEventPayload>>();
        private readonly Dictionary<string, Type> _eventTypesBindings = new Dictionary<string, Type>();

        protected void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        public void ProcessEvent(string serializedEvent)
        {
            var eventHeader = JsonUtility.FromJson<UIEventHeader>(serializedEvent);

            if (_eventBindings.ContainsKey(eventHeader.eventName))
            {
                var callback = _eventBindings[eventHeader.eventName];
                var receivedEvent = (UIEventPayload) JsonUtility.FromJson(eventHeader.payload, _eventTypesBindings[eventHeader.eventName]);
                callback.Invoke(eventHeader.eventName, receivedEvent);
            }
        }

        public void RegisterHandler<T>(string eventName, Action<string, UIEventPayload> handler)
            where T : UIEventPayload
        {
            if (_eventBindings.ContainsKey(eventName))
            {
                _eventBindings[eventName] = handler;
                _eventTypesBindings[eventName] = typeof(T);
            }
            else
            {
                _eventBindings.Add(eventName, handler);
                _eventTypesBindings.Add(eventName, typeof(T));
            }
        }

        public void UnregisterHandler(string eventName)
        {
            if (_eventBindings.ContainsKey(eventName))
            {
                _eventBindings.Remove(eventName);
                _eventTypesBindings.Remove(eventName);
            }
        }

        public void Dispatch<T>(UIEvent<T> uiEvent) where T : UIEventPayload
        {
            UIBridge.FireEvent(JsonUtility.ToJson(uiEvent));
        }
    }
}