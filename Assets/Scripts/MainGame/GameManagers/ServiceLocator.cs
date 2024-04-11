using UnityEngine;
using static MainGame.Singletons.MahadLibShortcuts;

namespace MainGame.GameManagers
{
    [DefaultExecutionOrder(-1)]
    [RequireComponent(typeof(InputEventManager),typeof(UIEventsManager))]
    public class ServiceLocator : MonoBehaviour
    {
        [HideInInspector] public InputEventManager inputEventManager;
        [HideInInspector] public UIEventsManager uiEventsManager;
        private void Awake()
        {
            InitSingleton(this);
            inputEventManager = GetComponent<InputEventManager>();
            uiEventsManager = GetComponent<UIEventsManager>();
        }
        
    }
}
