using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///掉落尖刺碰到地面后 停止且取消伤害
/// </summary>作者：青瓜
public class DropSpikeCollider :Collider2DBase
{
    public GameObject damager;
    public float gScale;
    protected override void enterEvent()//碰到ground layer
    {
        damager.SetActive(false);
    }

    public void drop()//开始掉落
    {
        GetComponent<Rigidbody2D>().gravityScale = gScale;
        damager.SetActive(true);
    }
}
