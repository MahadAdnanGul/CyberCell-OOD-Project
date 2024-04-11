using MainGame.GameManagers;
using UnityEngine;
using static MainGame.Singletons.Shortcuts;

namespace MainGame
{
    public class DeadZon : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Get<ServiceLocator>().uiEventsManager.onGameOver?.Invoke();
        }
    }
}
