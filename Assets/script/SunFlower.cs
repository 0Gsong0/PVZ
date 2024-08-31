using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : MonoBehaviour
{
    public GameObject Sun;
    private Animator  animator;
    private float readyTime = 6;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= readyTime)
        {
            animator.SetBool("Ready",true);
        }
    }
    public void SunOver()
    {
        timer = 0;
        animator.SetBool("Ready",false);
        BurnSun();
    }
    public void BurnSun()
    {
        GameObject newSun = Instantiate(Sun);
        float s = Random.Range(0, 2);
        float X = 1;
        if(s == 0)
        {
            X = Random.Range(transform.position.x - 30, transform.position.x - 20);
        }
        else if (s == 1) 
        {
            X = Random.Range(transform.position.x + 20, transform.position.x + 30);
        }
        float Y = Random.Range(transform.position.y - 20, transform.position.y + 20);
        newSun.transform.position = new Vector2(X, Y);
    }
}
