using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    private float Duation = 5;
    private float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > Duation)
        {
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        GameManager.Instance.ChangeSunNum(25);
        GameObject.Destroy(gameObject);
        //·Éµ½UIÇø

    }
}
