using UnityEngine;

namespace MainGame.CharacterEffects
{
    public class Explosion : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject,1f);
        }

    
    }
}
