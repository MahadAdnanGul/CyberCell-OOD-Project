using MainGame.GameConstants;
using MainGame.GameManagers;
using UnityEngine;
using static MainGame.Singletons.MahadLibShortcuts;

namespace MainGame.Gameplay.Player
{
    public class PlayerInputListener : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                Get<ServiceLocator>().inputEventManager.onMoveInputEvent?.Invoke(Direction.Left);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Get<ServiceLocator>().inputEventManager.onMoveInputEvent?.Invoke(Direction.Right);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Get<ServiceLocator>().inputEventManager.onJumpEvent?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Get<ServiceLocator>().inputEventManager.onDashEvent?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                Get<ServiceLocator>().inputEventManager.onShootEvent?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Get<ServiceLocator>().inputEventManager.onMeleeEvent?.Invoke();
            }
        
        

            if (Input.GetKeyDown(KeyCode.X))
            {
                Get<ServiceLocator>().inputEventManager.onSuperEvent?.Invoke();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Get<ServiceLocator>().inputEventManager.onItemSlotEvent?.Invoke(1);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    Get<ServiceLocator>().inputEventManager.onItemSlotEvent?.Invoke(2);
                }
                else
                {
                    Get<ServiceLocator>().inputEventManager.onItemSlotEvent?.Invoke(3);
                }
            }
        
        }
    }
}
