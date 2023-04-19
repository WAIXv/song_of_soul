using UnityEngine;
/// <summary>
/// ���� ����������ҵĴ��ʹ�����
/// </summary>���ߣ�Nothing
public class WhirlPool : SceneTransitionPoint
{
    private bool canTrans;
    public int cnt = 1;

    void Update()
    {
        if (canTrans)
        {
            enterEvent();       //�ƶ���ָ����������
            cnt = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO:��ұ����붯��
            canTrans = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canTrans = false;
            cnt = 1;
        }
    }
}
