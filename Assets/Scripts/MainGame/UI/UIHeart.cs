using UnityEngine;
using UnityEngine.UI;

namespace MainGame.UI
{
    public class UIHeart : MonoBehaviour
    {
        [SerializeField] private Image heartImage;
        [SerializeField] private Color fullColor;
        [SerializeField] private Color emptyColor;

        private bool isFull = true;

        public bool IsFull => isFull;

        public void ModifyHeart(bool isHealed)
        {
            heartImage.color = isHealed ? fullColor : emptyColor;
            isFull = isHealed;
        }
    }
}
