using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int SunNum;

    public GameObject bornParent;
    public GameObject ZombiePrefab;
    public float bornWait = 5;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        SunNum = 50;

        UIManager.instance.InitUI();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnZombie();
    }
    public void ChangeSunNum(int num)
    {
        SunNum += num;
        if (SunNum <= 0)
            SunNum = 0;
        //当数量改变调用UI更新
        UIManager.instance.UpdateUI();
    }
    private void SpawnZombie()
    {
        timer += Time.deltaTime;
        if(timer >= bornWait)
        {
            timer = 0;
            GameObject zombie = GameManager.Instantiate(ZombiePrefab);
            int index = Random.Range(0, 5);
            Transform parent = bornParent.transform.Find("born" + index.ToString());
            zombie.transform.parent = parent;
            zombie.transform.localPosition = Vector3.zero;
        }
    }
}
