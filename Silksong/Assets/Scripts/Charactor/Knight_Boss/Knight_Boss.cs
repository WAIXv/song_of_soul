using UnityEngine;

namespace Charactor.Knight_Boss
{
    public class Knight_Boss : MonoBehaviour
    {
        public Animator _anim;
        public Rigidbody2D _rigid;
        public Damable _damagable;
        public Transform _playerTrans;

        private PlayerController _player;

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _rigid = GetComponent<Rigidbody2D>();
            _damagable = GetComponent<Damable>();
            _player = GetComponent<PlayerController>();
            _playerTrans = _player.transform;
        }
        
        
    }
}