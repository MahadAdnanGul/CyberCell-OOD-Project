using System.Collections.Generic;
using MainGame.GameManagers;
using MainGame.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using static MainGame.Singletons.MahadLibShortcuts;

namespace MainGame.UI
{
    public class HealthUI : MonoBehaviour,ISubscriber
    {
        [SerializeField] private Slider superSlider;
        [SerializeField] private UIHeart heartPrefab;
        [SerializeField] private GameObject heartParent;
        private List<UIHeart> hearts = new List<UIHeart>();
        private void InitializeHearts(int count)
        {
            for (int i = 0; i < count; i++)
            {
                UIHeart heart = Instantiate(heartPrefab,heartParent.transform);
                hearts.Add(heart);
            }
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        private void OnDamageTaken(int damageAmount)
        {
            int numberOfReductions = damageAmount;
            for (int i = hearts.Count - 1; i >= 0; i--)
            {
                if (numberOfReductions == 0)
                {
                    return;
                }
                if (hearts[i].IsFull)
                {
                    numberOfReductions--;
                    hearts[i].ModifyHeart(false);
                }
            }
        }

        private void OnSuperUsed()
        {
            superSlider.value = 0;
        }

        private void OnHealed(int healAmount)
        {
            int numberOfIncrements = healAmount;
            for (int i = 0; i < hearts.Count; i++)
            {
                if (numberOfIncrements == 0)
                {
                    return;
                }
                if (!hearts[i].IsFull)
                {
                    numberOfIncrements--;
                    hearts[i].ModifyHeart(true);
                }
            }
        }

        private void OnEnemyDamaged(int damageAmount)
        {
            superSlider.value += damageAmount;
        }

        public void SubscribeEvents()
        {
            Get<ServiceLocator>().uiEventsManager.onDamageTaken += OnDamageTaken;
            Get<ServiceLocator>().uiEventsManager.onHealed += OnHealed;
            Get<ServiceLocator>().uiEventsManager.onEnemyDamaged += OnEnemyDamaged;
            Get<ServiceLocator>().uiEventsManager.onInitializeHud += InitializeHearts;
            Get<ServiceLocator>().uiEventsManager.onSuperUsed += OnSuperUsed;
        }

        public void UnsubscribeEvents()
        {
            Get<ServiceLocator>().uiEventsManager.onDamageTaken -= OnDamageTaken;
            Get<ServiceLocator>().uiEventsManager.onHealed -= OnHealed;
            Get<ServiceLocator>().uiEventsManager.onEnemyDamaged -= OnEnemyDamaged;
            Get<ServiceLocator>().uiEventsManager.onInitializeHud -= InitializeHearts;
            Get<ServiceLocator>().uiEventsManager.onSuperUsed -= OnSuperUsed;
        }
    }
}
