using UnityEngine;

namespace MainGame.CharacterEffects
{
    public class EnemyHurtEffect : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 0.25f;
        [SerializeField] private Color fadeColor;
        private SpriteRenderer sr;
        private Color initialColor;
        private int tweenId = -1;
        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            initialColor = sr.color;
        }

        public void PlayEffect()
        {
            LeanTween.Framework.LeanTween.cancel(gameObject,tweenId);
            sr.color = initialColor;
            tweenId = LeanTween.Framework.LeanTween.color(gameObject, fadeColor, fadeTime).setLoopPingPong(1).id;
        }
    }
}
