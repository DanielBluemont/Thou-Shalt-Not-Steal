using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    [SerializeField] private float force = 30f;
    [SerializeField] private int dmg = 40;
    bool canDmg = false;
    private void Start()
    {
        EventManager.onShoot += HandleBool;
    }
    private void OnDestroy()
    {
        EventManager.onShoot -= HandleBool;
    }
    private void HandleBool(bool state)
    {
        canDmg = state;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDmg)
        {

            Rigidbody2D rb = collision.attachedRigidbody;
            if (rb != null)
            {
                Vector2 direction = (rb.transform.position - transform.position).normalized;
                rb.AddForce(direction * force, ForceMode2D.Impulse);
                rb.GetComponent<NPC>().TakeDamage(dmg);
            }

        }
    }
   

    
}
