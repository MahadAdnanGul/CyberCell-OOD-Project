using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MahadLib;
using static MahadLibShortcuts;

[DefaultExecutionOrder(-1)]
[RequireComponent(typeof(InputEventManager),typeof(UIEventsManager))]
public class ServiceLocator : MonoBehaviour
{
    [HideInInspector] public InputEventManager inputEventManager;
    [HideInInspector] public UIEventsManager uiEventsManager;
    private void Awake()
    {
        InitSingleton(this);
        inputEventManager = GetComponent<InputEventManager>();
        uiEventsManager = GetComponent<UIEventsManager>();
    }
        
}
