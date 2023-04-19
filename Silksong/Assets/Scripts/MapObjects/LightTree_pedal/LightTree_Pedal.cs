using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTree_Pedal : MonoBehaviour
{
    public float CD;
    private bool isGrow = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //��ʱ�ķ�������boss��ɺ��滻Ϊ���boss�Ƿ��ڳ�����
        GameObject player = GameObject.Find("Player");
        if (player == null && isGrow == false)
        {
            StartCoroutine(GrowTree());
            isGrow = true;
        }

    }

    private IEnumerator GrowTree()
    {
        GameObject go = GameObject.Find("LightTree");
        List<Transform> lst = new List<Transform>();
        foreach (Transform child in go.transform)
        {
            lst.Add(child);
            Debug.Log(child.gameObject.name);
        }

        int i = 0;

        while (i < lst.Count)
        {
            yield return new WaitForSeconds(CD);//��CDΪ�������ƽ̨
            Debug.Log("���ֵ�ƽ̨�ǣ�" + lst[i].gameObject);
            lst[i].gameObject.layer = 6;
            i++;
        }
    }

}
