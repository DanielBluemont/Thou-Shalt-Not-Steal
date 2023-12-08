using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : NPC
{
    private void Awake()
    {
        hp_max = 200;
        hp_min = 150;
        m_max = 30;
        m_min = 0;
    }
    
}
