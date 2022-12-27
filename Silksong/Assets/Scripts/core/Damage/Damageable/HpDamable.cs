using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.Events;
/// <summary>
/// 
/// ӵ������ֵ����ط�����damable 
/// </summary>���ߣ����
public class HpDamable :Damable
{
    [SerializeField]
    private int maxHp ;//�������ֵ
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }

    [SerializeField]
    private int currentHp;//��ǰhp
    public int CurrentHp
    {
        get { return currentHp; }
    }
    

    public bool resetHealthOnSceneReload;

    [Serializable]
    public class dieEvent : UnityEvent<DamagerBase, DamageableBase>
    { }

    //[Serializable]
    public class setHpEvent : UnityEvent<HpDamable>
    { }

    public dieEvent onDieEvent;

    public setHpEvent onHpChange=new setHpEvent();

    public AudioCue dieAudio;//��audiomanager���а󶨹���hpdamableĬ�ϡ��ܻ�����Ч��Ч��


    public override void takeDamage(DamagerBase damager)
    {
        if ( currentHp <= 0)
        {
           // return;
        }

        base.takeDamage(damager);
        addHp(-damager.getDamage(this),damager);

    }


    public void setCurrentHp(int hp,DamagerBase damager=null)
    {
        currentHp = Mathf.Clamp(hp,0,MaxHp);
        onHpChange.Invoke(this);
        if (currentHp == 0)
        {
            die(damager);
        }
    }

    public void addHp(int number,DamagerBase damager)//���ܵ��˺� number<0
    {
        setCurrentHp(currentHp + number,damager);
    }

    protected virtual void die(DamagerBase damager)
    {
        onDieEvent.Invoke(damager,this);

        if(gameObject.tag!="Player")//reborn player for  test
        Destroy(gameObject);//δ����

        Debug.Log(gameObject.name+" die");
        if (dieAudio)
        {
            dieAudio.PlayAudioCue();
        }

    }

    public void BeAtcBySoulSkillAction(object obj)
    {
        SoulSkill soulSkill = (SoulSkill)obj;
        switch (soulSkill.atcEventName)
        {
            case "LightningChain Attack":
                
                break;
            default:
                break;
        }
        /*
        if(monster.GetComponent<MonsterInfo>().monsterName == monsterInfo.monsterName){
            string name = monsterInfo.monsterName;
            string playerName = player.GetComponent<PlayerInfo>().playerName;
            monsterInfo.hp -= player.GetComponent<PlayerInfo>().atk;
            anim.SetTrigger("beHit");
            if(monsterInfo.hp <= 0){
                anim.SetBool("isDie",true);
                this.GetComponent<Collider>().isTrigger = true;
                this.gameObject.layer = 0;
                rb.useGravity = false;
            }
            Debug.Log(name+" be atked by "+playerName+" HP: "+monsterInfo.hp);
            AudioManagerMy.Instance.PlayAudio("hit_05");
        }
        */
    }

}
