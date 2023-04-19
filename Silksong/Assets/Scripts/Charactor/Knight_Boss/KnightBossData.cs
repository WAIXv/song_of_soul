using UnityEngine;

namespace Charactor.Knight_Boss
{
    [CreateAssetMenu(fileName = "KnightBossData", menuName = "Game/KnightBossData")]
    public class KnightBossData : ScriptableObject
    {
        [Header("Move")] 
        public float MoveSpeed;

        [Header("Thrust")] 
        public float AttackDis;
    }
}