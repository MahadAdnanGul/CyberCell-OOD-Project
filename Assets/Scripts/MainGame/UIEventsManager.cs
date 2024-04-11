using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIEventsManager : MonoBehaviour
{
    public Action<int> onHealed;
    public Action<int> onDamageTaken;
    public Action<int> onEnemyDamaged;
    public Action<int> onInitializeHud;
    public Action onSuperUsed;
    public Action<Sprite, int> onItemAdded;
    public Action<int> onItemConsumed;
    public Action<float> onInvincible;
    public Action<float> onWeaponUpgrade;
    public Action onGameOver;
    public Action onGameWon;
}
