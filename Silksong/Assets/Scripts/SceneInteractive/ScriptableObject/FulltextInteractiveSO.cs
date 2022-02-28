using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "fulltext", menuName = "Interactive/fulltext")]
public class FulltextInteractiveSO : InteractiveSO
{
    [Tooltip("The type of the item")]
    [SerializeField] private EInteractiveItemType _itemType = EInteractiveItemType.FULLTEXT;
    public override EInteractiveItemType ItemType => _itemType;
}
