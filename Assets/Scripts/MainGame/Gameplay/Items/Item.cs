using UnityEngine;

namespace MainGame.Gameplay.Items
{
    public abstract class Item : ScriptableObject
    {
        public abstract void UseItem();
        public Sprite sprite;
    
    }
}
