using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(Animator))]
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverWindow;
        [SerializeField] private DestroyingObject _defenceCircle;
        [SerializeField] private UnityEvent _onDead;

        [Space(30)]
        [Header("Required Components:")]
        [Space(5)]
        [SerializeField] private Animator _animator;

        private bool _mortal = true;

        [Button]
        private void SetRequiredComponents()
        {
            _animator = GetComponent<Animator>();
        }

        public void TryApplyDamage()
        {
            if (_mortal)
            {
                StartDying();
            }
        }

        public void MakeImmortal()
        {
            _mortal = false;
            _defenceCircle.StartDestroying(() => _mortal = true, DestroyingType.SwitchingActivityOff);
        }

        private void StartDying()
        {
            if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Death") == false)
                _animator.SetTrigger("Die");
        }

        private void Die()
        {
            Time.timeScale = 0;
            _gameOverWindow.SetActive(true);
            _onDead?.Invoke();
        }
    }
}