using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript_Straight : MonoBehaviour
{
    [SerializeField]
    GameObject StraightBulletPrefab;

    [SerializeField]
    float startspeed = 0f;

    [SerializeField]
    float targetSpeed = 10f;

    [SerializeField]
    float speedUpTime;

    Collider2D coll;
    Transform bulletTransform;

    protected float currentSpeed;
    protected float smoothSpeed;

    public void SetBullet(Vector3 Position, Quaternion Rotation, float startspeed, float targetspeed, float speedupTime)
    {
        if (StraightBulletPrefab == null)
        {
            Debug.LogError("û��Ϊɢ���ӵ��ű�ָ��Ԥ����");
            return;
        }
        Instantiate(StraightBulletPrefab, Position, Rotation);
        BulletScript_Straight bullet_Script = StraightBulletPrefab.GetComponent<BulletScript_Straight>();
        bullet_Script.startspeed = startspeed;
        bullet_Script.targetSpeed = targetspeed;
        bullet_Script.speedUpTime = speedupTime;
    }

    private void Start()
    {
        bulletTransform = transform;
        coll = GetComponent<Collider2D>();
        currentSpeed = startspeed;
    }

    private void Update()
    {
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref smoothSpeed, Time.deltaTime * speedUpTime); //ƽ������
        Move();
    }

    private void Move()
    {

        bulletTransform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //����ܻ�
            Debug.Log("����ܻ�");
            Destroy(this.gameObject);
        }
        if (collision.gameObject.layer == 6)
        {
            Debug.Log("Bullet Hit Ground");
            Destroy(this.gameObject);
        }
    }
}

