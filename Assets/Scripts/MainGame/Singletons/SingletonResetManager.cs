using System;
using UnityEngine;

namespace MahadLib
{
    public class SingletonResetManager
    {
#if UNITY_EDITOR
        public static Action OnReset;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Reset()
        {
            OnReset?.Invoke();
            OnReset = null;
        }
#endif
    }
}