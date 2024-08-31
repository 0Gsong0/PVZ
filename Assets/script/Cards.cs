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
        curObject.transform.position = ScreenToWord(pointerEventData.position);//pointerEventData.position���λ��
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
        //OverlapPointAll ��ȡ����λ����������ײ��
        Collider2D[] col = Physics2D.OverlapPointAll(ScreenToWord(pointerEventData.position));

        foreach (Collider2D c in col)
        {
            //�����ײ��tag���Ƿ���ֲֲ����������������
            if(c.tag == "Land" && c.transform.childCount == 0)
            {
                //����ǰ��������Ϊ�������������
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
    //��Ļ����--����������
    static Vector3 ScreenToWord(Vector3 position)
    {
        Vector3 c = Camera.main.ScreenToWorldPoint(position);
        return new Vector3(c.x, c.y, 0);
    }   
}
