using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PlayerSkillManager : MonoBehaviour
{

    [Tooltip("The ScriptableObject that holds all the skills")]
    public SkillCollection skillCollection;

    /// <summary>
    /// ���ڼ����Ҫ�����ļ����Ƿ���skillCollection��
    /// </summary>
    Dictionary<PlayerSkill.SkillName, PlayerSkill> SkillDictionary;

    /// <summary>
    /// ����Ѿ����������м���
    /// </summary>
    public List<PlayerSkill> unlockedPlayerSkillList;

    /// <summary>
    /// ���װ���ļ���
    /// </summary>
    public PlayerSkill equippingPlayerSkill = null;

    /// <summary>
    /// ��¼�����Ƿ�����ȴ��
    /// �������ȴ�У���animator�Ĳ���SkillReadyΪfalse
    /// </summary>
    private bool isSkillInCoolDown = false;


    [Header("UI")]
    [SerializeField] Transform pfUnlockedSkillButton;
    [SerializeField] Transform UnlockedSkillContainer;
    [SerializeField] Text equippingSkillText;

    [SerializeField]
    private CharmListSO CharmListSO;


    private void Start()
    {
        isSkillInCoolDown = false;

        SkillDictionary = new Dictionary<PlayerSkill.SkillName, PlayerSkill>();
        foreach (PlayerSkill skill in skillCollection.AllSkills)
        {
            SkillDictionary[skill.Name] = skill;
        }

        // ���PlayerCanCastSkill()����false����animator�Ĳ���SkillReadyΪfalse
        // ��װ��һ���¼��ܵ�ʱ����ٽ���һ���ж�
        PlayerController playerController = gameObject.GetComponent<PlayerController>();
        playerController.PlayerAnimator.SetBool(playerController.animatorParamsMapping.SkillReadyParamHash, PlayerCanCastSkill());
    }

    private void Update()
    {
        // ����debug���Ժ����˾Ϳ���ɾ��
      /*  if (equippingPlayerSkill == null)
        {
            equippingSkillText.text = "null";
        }
        else
        {
            equippingSkillText.text = equippingPlayerSkill.Name.ToString();
        }*/
        
    }

    /// <summary>
    /// �������ܣ�����
    /// </summary>
    public void Cast()
    {
        switch (equippingPlayerSkill.Name)
        {
            default: break;

            case PlayerSkill.SkillName.VengefulSpirit:
                VengefulSpirit();
                break;
            case PlayerSkill.SkillName.DesolateDive:
                DesolateDive();
                break;
            case PlayerSkill.SkillName.DescendingDark:
                DescendingDark();
                break;
        }
        StartSkillCoolDown();
    }

    #region �����ܵĺ���

    /// <summary>
    /// ���ͷż��ܺ���ã����ܿ�ʼ��ȴ
    /// </summary>
    private void StartSkillCoolDown()
    {
        StartCoroutine(SkillCoolDownTimer(equippingPlayerSkill.CoolDown));
    }
    IEnumerator SkillCoolDownTimer(float cooldown)
    {
        PlayerController playerController = gameObject.GetComponent<PlayerController>();

        isSkillInCoolDown = true;
        playerController.PlayerAnimator.SetBool(playerController.animatorParamsMapping.SkillReadyParamHash, false);

        yield return new WaitForSeconds(cooldown);

        isSkillInCoolDown = false;
        playerController.PlayerAnimator.SetBool(playerController.animatorParamsMapping.SkillReadyParamHash, true);
        yield break;
    }

    /// <summary>
    /// ����Ƿ�����ͷż���
    /// ��animator�Ĳ���CanCastSkill����ͬһ������
    /// </summary>
    /// <returns>��������ͷż����򷵻�True�������ͷż����򷵻�False</returns>
    private bool PlayerCanCastSkill()
    {
        // ����Ƿ�װ���м��ܣ������Ƿ������˰�����prefab�������Ƿ�����ȴ
        // �����Ƿ�����PlayerStatusDic�е�PlayerStatusFlagWithMana�����ж�
        // ��Start()��ÿ��װ������ʱ�������

        if (equippingPlayerSkill.Name == PlayerSkill.SkillName.None)
        {
            Debug.LogWarning("You haven't equipped any skill!");
            return false;
        }
        else if (equippingPlayerSkill.SkillPrefab == null)
        {
            Debug.LogWarning("The prefab of the equipping skill is empty!");
            return false;
        }
        if (isSkillInCoolDown)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// װ��һ���ѽ����ļ���
    /// </summary>
    /// <param name="skill"></param>
    public void EquipSkill(PlayerSkill skill)
    {
        //equippingSkillText.text = skill.Name.ToString();
        equippingPlayerSkill = skill;

        // ���PlayerCanCastSkill()����false����animator�Ĳ���SkillReadyΪfalse
        PlayerController playerController = gameObject.GetComponent<PlayerController>();
        playerController.PlayerAnimator.SetBool(playerController.animatorParamsMapping.SkillReadyParamHash, PlayerCanCastSkill());
        //Debug.Log(equippingPlayerSkill.Name);
    }

    /// <summary>
    /// ����һ����SkillCollection�еļ���,�����ظ�����ͬһ����
    /// </summary>
    /// <param name="skillName">Ҫ�����ļ��ܵ�����</param>
    public void UnlockSkill(PlayerSkill.SkillName skillName)
    {
        if (SkillDictionary.ContainsKey(skillName) && !unlockedPlayerSkillList.Contains(SkillDictionary[skillName]))
        {
            unlockedPlayerSkillList.Add(SkillDictionary[skillName]);

            Transform skillbutton = Instantiate(pfUnlockedSkillButton, UnlockedSkillContainer);
            skillbutton.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = skillName.ToString();
            skillbutton.gameObject.GetComponent<UnlockedSkillButton>().EquipSkill += () => { EquipSkill(SkillDictionary[skillName]); };
        }
    }

    #endregion

    #region ����
    // ����ļ���Ч����ʵ�֣���Cast()�����

    private void VengefulSpirit()
    {
        Debug.Log("Casting Vengeful Spirit");

        Transform skillTransform = Instantiate(equippingPlayerSkill.SkillPrefab, this.transform.position, Quaternion.identity);
        skillTransform.gameObject.GetComponent<PlayerSkillDamager>().damage = equippingPlayerSkill.Damage;

        Vector3 shootDirection = new Vector3(PlayerController.Instance.playerInfo.playerFacingRight ? 1 : -1, 0, 0);
        skillTransform.gameObject.GetComponent<Rigidbody2D>().AddForce(shootDirection * 15f, ForceMode2D.Impulse);

        Destroy(skillTransform.gameObject, 2f);
    }

    private void DesolateDive()
    {
        Debug.Log("Casting Desolate Dive");
    }

    private void DescendingDark()
    {

        Debug.Log("Casting Descending Dark");
    }



    #endregion

    #region ���ڲ��Եĺ���
    public void testUnlockDesolateDive()
    {
        //print("testing unlock skill");
        UnlockSkill(PlayerSkill.SkillName.DesolateDive);
    }
    public void testUnlockDecendingDark()
    {
        //print("testing unlock skill");
        UnlockSkill(PlayerSkill.SkillName.DescendingDark);
    }
    public void testUnlockVengefulSpirit()
    {
        //print("testing unlock skill");
        UnlockSkill(PlayerSkill.SkillName.VengefulSpirit);
    }
    #endregion
}
