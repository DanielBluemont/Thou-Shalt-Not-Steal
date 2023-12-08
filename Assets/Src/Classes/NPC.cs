using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public delegate void OnDeath();
    public event OnDeath OnDeathEvent
        ;
    protected int hp, hp_max, hp_min;
    protected int money, m_max, m_min;
    protected Rigidbody2D rb;
    protected SpriteRenderer SpriteR;
    [SerializeField] protected Sprite knockout;
    [SerializeField] protected SpriteRenderer dollars;
    protected NavMeshAgent agent;
    protected bool isRunnig = false;
    protected bool isDead = false;
    Points pointsArr;

    public int StealMoney()
    {
        dollars.enabled = false;
        int m = money;
        money = 0;
        StartCoroutine(FadeAway());
        return m;
    }
    public virtual void Start()
    {
        NavInit();
        pointsArr = FindObjectOfType<Points>();
        dollars.enabled = false;
        SpriteR = this.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        GenerateHP();
        GenerateMoney();
        Wander();
    }
    protected void NavInit()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    public virtual void Update()
    {
        if (!isDead ) 
        {
            if (IsPathCompleted())
            {
                Wander();
            }
        }
        



    }
   
    protected void Wander()
    {
        agent.SetDestination(pointsArr.GetFarthestPoint(this.transform));
    }
    protected bool IsPathCompleted()
    {
        return !agent.pathPending && !agent.hasPath;
    }
    protected IEnumerator FadeAway()
    {
        float time = 0;
        while (time < 3)
        {
            time += Time.deltaTime;
            float r = time / 3;
            r = Mathf.Lerp(1, 0, r);
            SpriteR.color = new Color(1, 1, 1, r);
            yield return null;
        }
        OnDeathEvent?.Invoke();
        Destroy(this.gameObject);
    }
    protected IEnumerator KnockBack()
    {
        yield return new WaitForSeconds(0.4f);
        rb.velocity = Vector3.zero;

    }
    // Start is called before the first frame update
    protected void GenerateHP()
    {
        hp = Random.Range(hp_min, hp_max);
    }
    protected void GenerateMoney()
    {
        money = Random.Range(m_min, m_max);
    }
    public virtual void TakeDamage(int dmg)
    {
        StartCoroutine(KnockBack());
        StartCoroutine(ColorChange());
        hp -= dmg;
        if (hp <= 0)
            Die();
        
        
    }
    protected IEnumerator ColorChange()
    {
        SpriteR.color = new Color32(241, 44, 44, 255);
        yield return new WaitForSeconds(0.2f);
        SpriteR.color = Color.white;
    }
    private void Die()
    {
        
        isDead = true;
        agent.isStopped = true;
        agent.ResetPath();
        dollars.enabled = true;
        SpriteR.sprite = knockout;
        transform.rotation = Quaternion.Euler(0, 0, -90);
        gameObject.layer = LayerMask.NameToLayer("dead");
    }
   
}
