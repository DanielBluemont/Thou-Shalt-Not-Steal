using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.VFX;

public class Points : MonoBehaviour
{
    [SerializeField] private int enemiesRemaining = 5;
    
    

    [SerializeField] private List<Transform> WayPoints = new List<Transform>();
    List<PointDis> allDist = new List<PointDis>();
    int elementsCount;
    float rate = 3f;
    [SerializeField] private GameObject[] enemies = new GameObject[2];
    private void Start()
    {
        elementsCount = WayPoints.Count;
        StartCoroutine(Spawn());
    }
    IEnumerator Spawn() 
    {
        for (int i = 0; i < 5; i++)
        {
            int ran = Random.Range(0, 2);
            int pr = Random.Range(0, elementsCount);
            Instantiate(enemies[ran], WayPoints[pr].position, Quaternion.identity).GetComponent<NPC>().OnDeathEvent += OnDestroyedEnemy;
            enemiesRemaining++;
            yield return new WaitForSeconds(rate); 
        }
      
    }
    private void OnDestroyedEnemy()
    {
        enemiesRemaining--;

        if (enemiesRemaining <= 0)
        {
            // All previous enemies are destroyed, spawn a new wave.
            StartCoroutine(Spawn());
        }
    }
        public Vector2 GetFarthestPoint(Transform entity)
    {

        List<PointDis> top3 = new List<PointDis>(3);
        for (int i = 0; i < elementsCount; i++)
        {
            PointDis v = new PointDis(WayPoints[i].position, Vector2.Distance(entity.position, WayPoints[i].position));
            allDist.Add(v);
        }
        top3 = allDist.OrderByDescending(x=> x.distance).Take(3).ToList();
        return top3[Random.Range(0,3)].pos;
    }
}

public struct PointDis
{
    public float distance;
    public Vector2 pos;
    public PointDis(Vector2 _point, float _dis)
    {
        pos = _point;
        distance = _dis;
    }
}
