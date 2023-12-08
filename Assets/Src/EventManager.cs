using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager 
{
    public delegate void Shoot(bool state);
    public static event Shoot onShoot;
    
    public static void CallShoot(bool state)
    {
        onShoot?.Invoke(state);
    }
}
