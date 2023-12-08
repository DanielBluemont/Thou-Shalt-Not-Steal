using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Fist : MonoBehaviour
{
    

    [SerializeField] private Transform hand;
    [SerializeField] private float startDis = 1.5f;
    [SerializeField] private float endDis = 3;
    float distance;
    Camera main_cam;
    Transform fistGun;
    Vector3 scale;
    bool Right = true;
    bool isAttacking = false;
    float dur = 0.3f;



    void Awake()
    {
        main_cam = Camera.main;
        fistGun = hand.GetComponentInChildren<Transform>();
        scale = fistGun.localScale;
        distance = startDis;
    }

    // Update is called once per frame
    void Update()
    {
        RotateGun();
        Punch();
    }
    private void RotateGun()
    {
        Vector3 pos = main_cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = pos - transform.position;
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
        fistGun.rotation = Quaternion.Euler(new Vector3(0, 0,angle));
        fistGun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(distance, 0 , 0);
        CheckFlip(pos);
    }
    private void Punch()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        { 
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        isAttacking = true;
        EventManager.CallShoot(isAttacking);
        float time = 0;
        while (time < dur)
        {
            time += Time.deltaTime;
            float r = time/dur;
            distance = Mathf.Lerp(startDis, endDis, r);
            yield return null;
        }
        time = 0;
        EventManager.CallShoot(!isAttacking);
        while (time < dur)
        { 
            time += Time.deltaTime;
            float r = time / dur;
            distance = Mathf.Lerp(endDis, startDis, r);
            yield return null;
        }
        isAttacking = false;
    }
    void CheckFlip(Vector3 pos)
    {
        if (pos.x < fistGun.position.x && Right)
            FlipSprite();
        else if (pos.x > fistGun.position.x && !Right) 
            FlipSprite();
    }
    void FlipSprite()
    {
        Right = !Right;
        fistGun.localScale = new Vector3(fistGun.localScale.x, fistGun.localScale.y * -1, fistGun.localScale.z);
    }
}
