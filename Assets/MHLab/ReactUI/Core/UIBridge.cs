using System.Runtime.InteropServices;
using UnityEngine;

namespace MHLab.ReactUI.Core
{
    public class UIBridge
    {
#if UNITY_EDITOR
        public static void DebugLog(string message)
        {
            Debug.Log(message);
        }
#else
    [DllImport("__Internal")]
    public static extern void DebugLog(string message);
#endif

#if UNITY_EDITOR
        public static void FireEvent(string serializedEvent)
        {
            Debug.Log(serializedEvent);
        }
#else
    [DllImport("__Internal")]
    public static extern void FireEvent(string serializedEvent);
#endif
    }
}