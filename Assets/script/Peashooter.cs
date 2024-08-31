using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Peashooter : MonoBehaviour
{
    public float interval;
    public float timer;
    public GameObject bullet;
    public Transform bulletPos;

    public float health = 100;
    public float curHealth;
    private void Start()
    {
        curHealth = health;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            timer = 0;
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        }
    }
    public float ChangeHeath(float num)
    {
        curHealth = Mathf.Clamp(curHealth + num, 0, health);
        if(curHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }
        return curHealth;
    }
}
