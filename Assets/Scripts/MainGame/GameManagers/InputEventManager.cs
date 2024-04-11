using System;
using MainGame.GameConstants;
using UnityEngine;

namespace MainGame.GameManagers
{
    public class InputEventManager : MonoBehaviour
    {
        public Action<Direction> onMoveInputEvent;
        public Action onJumpEvent;
        public Action onDashEvent;
        public Action onShootEvent;
        public Action onMeleeEvent;
        public Action onSuperEvent;

        public Action<int> onItemSlotEvent;
    }
}
