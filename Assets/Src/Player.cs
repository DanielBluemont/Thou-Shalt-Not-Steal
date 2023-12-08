using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    private float speed = 5;
    [SerializeField] private TextMeshProUGUI m_text, g_text, prompt;
    Rigidbody2D rb;
    Vector2 pos;
    Animator animator;
    float norSpeed = 5;
    float maxSpeed = 8;
    int currentMoney = 0;
    int given = 0;
    LayerMask layerD;
    float t = 0;
    float gift = 10;
    void Start()
    {
        prompt.enabled = false;
        layerD = LayerMask.GetMask("dead");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Gift()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, 2f, LayerMask.GetMask("Poor"));
            Poor p = col.GetComponent<Poor>();

            if (col == null || p == null) return;

            t += Time.deltaTime;
            if (t >= 0.01f)
            {
                if (currentMoney - gift >= 0)
                {
                    p.GiveMoney((int)gift);
                    currentMoney -= (int)gift;
                    given += (int)gift;
                    UpdateMoney();
                }
                t = 0;
            }
        }
        
    }
    
    private void CollectMoney()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, 2f, layerD);
        if (col != null )
        {
            prompt.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                currentMoney += col.GetComponent<NPC>().StealMoney();
                UpdateMoney();
                prompt.enabled = false;
            }
        }
        else
            prompt.enabled = false;

    }
    private void UpdateMoney()
    {
        Debug.Log(currentMoney);
        m_text.text = currentMoney.ToString();
        g_text.text = given.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        pos.x = Input.GetAxisRaw("Horizontal");
        pos.y = Input.GetAxisRaw("Vertical");
        pos = pos.normalized* speed;
        animator.SetFloat("velocity", pos.magnitude);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = maxSpeed;
        }
        else
        {
            speed = norSpeed; 
        }
        CollectMoney();
        Gift();
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + pos * Time.deltaTime);
    }
}
