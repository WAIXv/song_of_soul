using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingBullet : BulletCollision
{
    private float angle;
    private PlayerCharacter player;
    [SerializeField]
    private float Speed = 1;
    [SerializeField]
    float minBall = 25f;
    [SerializeField]
    float maxBall = 50f;

    float BallisticAngle;

    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            StartCoroutine(TrackingPlayer(player));

        }
        else
        {
            print("û���ҵ����");
            player = FindObjectOfType<PlayerCharacter>();
        }

    }


    IEnumerator TrackingPlayer(PlayerCharacter player)
    {
        BallisticAngle = Random.Range(minBall, maxBall);
        Vector3 targetDirecton = player.transform.position - this.transform.position;//��ȡ���λ��
        angle = Mathf.Atan2(targetDirecton.y, targetDirecton.x) * Mathf.Rad2Deg;//װ��Ϊ��ת�Ǹ��ⵯ
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.Translate(Vector3.right * Speed * 2 * Time.deltaTime);
        transform.rotation *= Quaternion.Euler(0f, 0f, BallisticAngle);//������Ƕȸ��ⵯ ʹ�ⵯ���й켣�л���
        transform.Translate(Vector3.right * Speed * 1.1f * Time.deltaTime);
        yield return null;
    }

}
