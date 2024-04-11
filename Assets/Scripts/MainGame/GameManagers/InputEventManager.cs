using System;
using System.Collections;
using System.Collections.Generic;
using Mahad.GameConstants;
using UnityEngine;

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
