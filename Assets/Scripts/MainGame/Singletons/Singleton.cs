using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

namespace MainGame.Singletons
{
    /// <summary>
    /// Singleton class allowing to create singleton of basically anything
    /// Add using static YorfLibCore to use this class like this Get<X>() & InitSingleton(x)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class
    {
        private static T s_instance;

        public static void Reset()
        {
            s_instance = null;
        }

        public static void Initialise(T instance, bool resetOnPlay = true)
        {
            if (instance == null)
            {
                Debug.LogError("Singleton: " + typeof(T).Name + " was initialized with a null value!");
                return;
            }

#if UNITY_EDITOR
            if (s_instance != null && EditorApplication.isPlayingOrWillChangePlaymode)
            {
                Debug.LogWarning("Singleton: " + typeof(T).Name + " already initialized!");
            }
#endif
            
            s_instance = instance;
            
#if UNITY_EDITOR
            SessionState.SetBool("singleton_" + typeof(T).FullName + "_resets", resetOnPlay);
            
            if(resetOnPlay)
            {
                EditorApplication.playModeStateChanged += OnPlayModeChanged;
                SingletonResetManager.OnReset += () => s_instance = null;
            }
#endif
        }
        
#if UNITY_EDITOR
        private static void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                s_instance = null;
                EditorApplication.playModeStateChanged -= OnPlayModeChanged;
            }
        }
#endif

        public static void InitialiseGeneric(object instance, bool resetOnPlay = true)
        {
            Initialise((T) instance, resetOnPlay);
        }

        public static T Instance
        {
            get
            {
#if UNITY_EDITOR
                if (s_instance == null)
                {
                    System.Type type = typeof(T);
                    RuntimeHelpers.RunClassConstructor(type.TypeHandle);

                    if (s_instance == null)
                    {
                        T findComponent = FindObject();
                        if (findComponent != null)
                        {
                            Initialise(findComponent, SessionState.GetBool("singleton_" + typeof(T).FullName + "_resets", true));
                        }
                        else
                        {
                            if (Application.isPlaying)
                            {
                                Debug.LogError("Singleton instance " + typeof(T).Name +
                                               " is null, searching for it. Fix this to prevent build error");
                            }
                        }
                    }
                }
#endif

                return s_instance;
            }
        }

        public static bool Exists
        {
            get
            {
                return s_instance != null;
            }
        }
        
        public static bool IsEqualsTo(object instance)
        {
            return s_instance == instance;
        }

#if UNITY_EDITOR
        private static T FindObject()
        {
            if (typeof(T).IsSubclassOf(typeof(ScriptableObject)))
            {
                Object[] resources = Resources.FindObjectsOfTypeAll(typeof(T));
                if(resources.Length > 0)
                {
                    return (T)(object)resources[0];
                }
                
                return null;
            }
 
            return default(T);
        }
#endif
    }

    /// <summary>
    /// Helper class for singleton access
    /// </summary>
    public static partial class MahadLibShortcuts
    {
        /// <summary>
        /// Get singleton
        /// </summary>
        /// <typeparam name="T">Singleton type</typeparam>
        /// <returns>Singleton instance</returns>
        public static T Get<T>() where T : class
        {
            return Singleton<T>.Instance;
        }

        /// <summary>
        /// Init singleton with the instance
        /// </summary>
        /// <param name="instance">Instance to set as singleton</param>
        /// <typeparam name="T">Type of singleton</typeparam>
        public static void InitSingleton<T>(T instance, bool resetExisting = false) where T : class
        {
            if (resetExisting)
            {
                Singleton<T>.Reset();
            }
            Singleton<T>.Initialise(instance);
        }
	
        /// <summary>
        /// Check is singleton is set
        /// </summary>
        /// <typeparam name="T">Type of singleton</typeparam>
        /// <returns>True if singleton exists, false otherwise</returns>
        public static bool IsSingletonSet<T>() where T : class
        {
            return Singleton<T>.Exists;
        }
    }
}