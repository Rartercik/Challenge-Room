using UnityEngine;

public abstract class AttackInstanceFactory : MonoBehaviour
{
    [SerializeField] int _minAttacksCount;
    [SerializeField] int _maxAttacksCount;
    [SerializeField] int _attacksCount;
    [SerializeField] int _attacksCounChangeFrequency;
    [SerializeField] int _attacksIncreaseCount;

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
        if (timeInSeconds % _attacksCounChangeFrequency == 0)
        {
            var increasedAttacksCount = _attacksCount + _attacksIncreaseCount;
            if (increasedAttacksCount < _maxAttacksCount)
                _attacksCount = increasedAttacksCount;
            else
                _attacksCount = _maxAttacksCount;
        }
    }
}
