using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Richman : NPC
{
    private void Awake()
    {
        hp_max = 150;
        hp_min = 90;
        m_max = 500;
        m_min = 400;
    }  
}
