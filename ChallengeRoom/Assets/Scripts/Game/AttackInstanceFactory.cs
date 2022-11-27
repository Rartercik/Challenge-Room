using UnityEngine;

namespace Game
{
    public abstract class AttackInstanceFactory : MonoBehaviour
    {
        [SerializeField] private int _minAttacksCount;
        [SerializeField] private int _maxAttacksCount;
        [SerializeField] private int _attacksCount;
        [SerializeField] private int _attacksCountChangeFrequency;
        [SerializeField] private int _attacksIncreaseCount;

        protected int MaxAttacksCount => _maxAttacksCount;

        public void UpdateFactory(int timeInSeconds)
        {
            UpdateAttacksCount(timeInSeconds);

            if (GetAttackAvailable())
            {
                var finalAttacksCount = Random.Range(_minAttacksCount, _attacksCount);
                Attack(finalAttacksCount);
            }
        }

        protected abstract void Attack(int attacksCount);

        protected abstract bool GetAttackAvailable();

        private void UpdateAttacksCount(int timeInSeconds)
        {
            if (timeInSeconds % _attacksCountChangeFrequency == 0)
            {
                var increasedAttacksCount = _attacksCount + _attacksIncreaseCount;
                _attacksCount = increasedAttacksCount < _maxAttacksCount ? increasedAttacksCount : _maxAttacksCount;
            }
        }
    }
}
