using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Poor : NPC
{
    private void Awake()
    {
        hp_max = 60;
        hp_min = 40;
        m_max = 30;
        m_min = 0;
    }
    public override void Start()
    {
        dollars.enabled = false;
        SpriteR = this.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        GenerateHP();
        GenerateMoney();
    }
    public override void Update()
    {
    }
    public override void TakeDamage(int dmg)
    {
     
        StartCoroutine(ColorChange());
        


    }
    public void GiveMoney(int _mon)
    {
        money += _mon;
    }
    
}
