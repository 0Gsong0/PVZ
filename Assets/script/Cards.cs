using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cards : MonoBehaviour
{
    public GameObject ObjectPrefab;
    private GameObject curObject;
    private GameObject Dark;
    private GameObject ProGress;
    public float waitTime;
    private float timer;
    public int useSun;
    // Start is called before the first frame update
    void Start()
    {
        Dark = transform.Find("Dark").gameObject;
        ProGress = transform.Find("ProGress").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateProGress();
        UpdateDark();
    }
    void UpdateProGress()
    {
        float per = math.clamp(timer/waitTime, 0, 1);
        ProGress.GetComponent<Image>().fillAmount = 1 - per;
    }
    void UpdateDark()
    {
        if(ProGress.GetComponent<Image>().fillAmount == 0 && GameManager.Instance.SunNum >= useSun)
        {
            Dark.SetActive(false);
        }
        else Dark.SetActive(true);
    }
    public void OnBeginDrag(BaseEventData data)
    {
        if (Dark.activeSelf) return;
        PointerEventData pointerEventData = data as PointerEventData;
        curObject = Instantiate(ObjectPrefab);
        curObject.transform.position = ScreenToWord(pointerEventData.position);//pointerEventData.position鼠标位置
    }
    public void OnDrag(BaseEventData data)
    {
        if (curObject == null) return;
        PointerEventData pointerEventData = data as PointerEventData;

        curObject.transform.position = ScreenToWord(pointerEventData.position);
    }
    public void OnEndDrag(BaseEventData data)
    {
        if (curObject == null) return;
        PointerEventData pointerEventData = data as PointerEventData;
        //OverlapPointAll 获取世界位置下所有碰撞体
        Collider2D[] col = Physics2D.OverlapPointAll(ScreenToWord(pointerEventData.position));

        foreach (Collider2D c in col)
        {
            //检测碰撞体tag和是否种植植物（检查子物体数量）
            if(c.tag == "Land" && c.transform.childCount == 0)
            {
                //将当前物体设置为地面格子子物体
                curObject.transform.parent = c.transform;
                curObject.transform.localPosition = Vector3.zero;

                curObject = null;
                GameManager.Instance.ChangeSunNum(-useSun);
                break;
            }
        }
        if (curObject != null)
        {
            GameObject.Destroy(curObject);
            curObject = null;
        }
    }
    //屏幕坐标--》世界坐标
    static Vector3 ScreenToWord(Vector3 position)
    {
        Vector3 c = Camera.main.ScreenToWorldPoint(position);
        return new Vector3(c.x, c.y, 0);
    }   
}
