using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Vector3 Dir;
    public float speed;
    public float damage = 30;
    private void Start()
    {
        GameObject.Destroy(gameObject,20);
    }
    void Update()
    {
        transform.position += Dir * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Zombie")
        {
            GameObject.Destroy (gameObject);
            collision.GetComponent<Zombie>().ChangeHeath(-damage);
        }
    }
}
