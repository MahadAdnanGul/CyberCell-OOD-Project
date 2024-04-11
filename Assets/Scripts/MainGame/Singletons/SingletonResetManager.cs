using System;
using UnityEngine;

namespace MainGame.Singletons
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