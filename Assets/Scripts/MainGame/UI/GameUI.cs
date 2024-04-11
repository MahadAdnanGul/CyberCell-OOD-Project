using MainGame.GameManagers;
using MainGame.Interfaces;
using UnityEngine;
using static MainGame.Singletons.Shortcuts;

namespace MainGame.UI
{
    public class GameUI : MonoBehaviour, ISubscriber
    {
        [SerializeField] private GameObject gameOver;
        [SerializeField] private GameObject gameWon;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void GameOver()
        {
            gameOver.SetActive(true);
        }

        private void GameWon()
        {
            gameWon.SetActive(true);
        }

        public void SubscribeEvents()
        {
            Get<ServiceLocator>().uiEventsManager.onGameOver += GameOver;
            Get<ServiceLocator>().uiEventsManager.onGameWon += GameWon;
        }

        public void UnsubscribeEvents()
        {
            Get<ServiceLocator>().uiEventsManager.onGameOver -= GameOver;
            Get<ServiceLocator>().uiEventsManager.onGameWon -= GameWon;
        }
    }
}
