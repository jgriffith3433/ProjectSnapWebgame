using System;
using System.Collections.Generic;
using MHLab.ReactUI.Core;
using UnityEngine;

namespace MHLab.ReactUI.Scripts
{
    [Serializable]
    public class UIInputSwitched : UIEventPayload
    {
        public bool isUnity;
    }

    public class UIInputSwitcher : MonoBehaviour
    {
        protected void Start()
        {
            UIEventDispatcher.Instance.RegisterHandler<UIInputSwitched>("UIInputSwitched", UIInputSwitched);
        }

        private void UIInputSwitched(string eventName, UIEventPayload e)
        {
            var uiInputSwitched = e as UIInputSwitched;
#if !UNITY_EDITOR && UNITY_WEBGL
    WebGLInput.captureAllKeyboardInput = uiInputSwitched.isUnity;
#endif
        }
    }
}