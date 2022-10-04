using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 通过SO存储所有的护符列表，并且存储玩家的获得情况与装备情况
/// </summary> 作者：次元

[CreateAssetMenu(fileName = "CharmList", menuName = "Charm/CharmList")]
public class CharmListSO : ScriptableObject
{
    [SerializeField] private List<CharmSO> Charms = new List<CharmSO>();

    
    private int charmAttackGainSoul;
    /// <summary>
    /// 护符攻击回能改变值
    /// </summary>
    public int CharmAttackGainSoul { get => charmAttackGainSoul; set => charmAttackGainSoul = value; }
    
    private int charmHurtGainSoul;
    /// <summary>
    /// 护符受伤回能改变值
    /// </summary>
    public int CharmHurtGainSoul { get => charmHurtGainSoul; set => charmHurtGainSoul = value; }

    /// <summary>
    /// 护符提供的临时血量
    /// </summary>
    public int CharmExtraHealth;

    /// <summary>
    /// 护符提供的攻击范围
    /// </summary>
    public float CharmAttackRange;

    /// <summary>
    /// 护符提供的攻击速度
    /// </summary>
    public float CharmAttackSpeed;

    /// <summary>
    /// 护符提供的移动速度
    /// </summary>
    public float CharmMoveSpeed;

    /// <summary>
    /// 护符提供的攻击伤害
    /// </summary>
    public float CharmAttackDamage;





    /// <summary>
    /// 获得护符
    /// </summary>
    /// <param name="name"> 护符名称 </param>
    /// <returns></returns>
    public bool CollectCharm(string name)
    {
        foreach (var item in Charms)
        {
            if (name.Equals(item.Name))
            {
                item.HasCollected = true;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 装备护符
    /// </summary>
    /// <param name="name"> 护符名称 </param>
    /// <returns></returns>
    public bool EquipCharm(string name)
    {
        foreach (var charm in Charms)
        {
            if (!charm.HasEquiped && name.Equals(charm.Name))
            {
                charm.HasEquiped = true;
                charm.OnEquip();
                return true;
            }
        }
        return false; //没有找到匹配的护符名称
    }

    /// <summary>
    /// 卸载护符
    /// </summary>
    /// <param name="name">护符名称</param>
    /// <returns></returns>
    public bool DisEquipCharm(string name)
    {
        foreach (CharmSO charm in Charms)
        {
            if (charm.HasEquiped && name.Equals(charm.Name))
            {
                charm.HasEquiped = false;
                charm.OnDisEquip();
                return true;
            }
        }
        return false;
    }


    /// <summary>
    /// 激活所有护符，在游戏开始后激活所有已装备的护符
    /// </summary>
    public void ActiveAllEquipedCharms()
    {
        foreach (var charm in Charms)
        {
            if (charm.HasEquiped)
            {
                charm.OnEquip();
            }
        }
    }
}




