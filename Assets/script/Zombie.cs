using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Vector3 Dir = new Vector3(-1,0,0);
    public float speed;
    private bool isWalk;
    private Animator animator;
    public float damage;
    public float damageWait;
    private float damageTimer;
    public float heath = 100;
    private float curHeath;
    private float lostHeadHeath = 40;
    private GameObject head;
    private bool lostHead;
    private bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        isWalk = true;
        animator = GetComponent<Animator>();

        curHeath = heath;
        head = transform.Find("head").gameObject;
        isDie = false;
        lostHead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie) return;
        Move();
    }
    private void Move()
    {
        if (!isWalk) return;
        transform.position += Dir * speed * Time.deltaTime;
    }
    public void ChangeHeath(float num)
    {
        curHeath = Mathf.Clamp(curHeath + num, 0, heath);
        if(curHeath <= lostHeadHeath && !lostHead)
        {
            lostHead = true;
            animator.SetBool("LostHead",true);
            head.SetActive(true);
        }
        if(curHeath <= 0)
        {
            animator.SetTrigger("Die");
            isDie = true;
        }
    }
    //Åö×²
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDie) return;
        if (collision.tag == "Plant")
        {
            isWalk = false;
            animator.SetBool("Walk",false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isDie) return;
        if (collision.tag == "Plant")
        {
            damageTimer += Time.deltaTime;
            if(damageTimer >= damageWait)
            {
                damageTimer = 0;
                //¹¥»÷Âß¼­
                Peashooter peashooter =  collision.GetComponent<Peashooter>();
                float ph = peashooter.ChangeHeath(-damage);
                if(ph <= 0)
                {
                    isWalk = true ;
                    animator.SetBool("Walk",true);
                } 
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isDie) return;
        if (collision.tag == "Plant")
        {
            isWalk = true ;
            animator.SetBool("Walk",true);
        }
    }
    private void DieOver()
    {
        animator.enabled = false;
        GameObject.Destroy(gameObject);
    }
}
