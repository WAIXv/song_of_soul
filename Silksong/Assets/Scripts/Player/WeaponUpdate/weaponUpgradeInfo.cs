using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct RequiredMaterial
{
    public string id;
    public int amonut;
}
[Serializable]
public class WeaponUpgradeInfo 
{
    public int level;//�ô�����ʱ�����ĵȼ�
    public int attack;
    public List<RequiredMaterial> requiredMaterial=new List<RequiredMaterial>() ;//�ô�����ʱ����Ĳ���id�Ͷ�Ӧ������


}
