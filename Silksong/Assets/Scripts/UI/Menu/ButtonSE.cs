using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �������������Ҫѡ����Ч�Ͱ�����Ч�İ�ť��
/// </summary>
public class ButtonSE : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartMenu.Instance.PlaySelectSoundEffect();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartMenu.Instance.PlayClickSoundEffect();
    }
}
