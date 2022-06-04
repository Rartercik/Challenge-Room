using System.Collections.Generic;
using UnityEngine;

public class SpikesFactory : AttackInstanceFactory
{
    [SerializeField] Transform _spikeInvokersParent;

    [SerializeField] int _spikesLifetimeInSeconds;

    private SpikeInvoker[] _spikeInvokers;

    private bool _canAttack = true;

    private void Start()
    {
        _spikeInvokers = _spikeInvokersParent.GetComponentsInChildren<SpikeInvoker>(false);
        if (MaxAttacksCount > _spikeInvokers.Length)
            throw new System.FieldAccessException("Max count of invoked spikes can`t be bigger than count of spikes");
    }

    protected override bool GetAttackAvailable()
    {
        return _canAttack;
    }

    protected override void Attack(int attacksCount)
    {
        var spikeInvokers = GetRandomSpikeInvokers(_spikeInvokers, attacksCount);

        foreach (var spikeInvoker in spikeInvokers)
            spikeInvoker.StartInvoke(_spikesLifetimeInSeconds, () => _canAttack = true);

        if(attacksCount > 0 && _canAttack)
            _canAttack = false;
    }

    private IEnumerable<SpikeInvoker> GetRandomSpikeInvokers(IEnumerable<SpikeInvoker> spikeInvokers, int count)
    {
        var list = new List<SpikeInvoker>(spikeInvokers);
        var result = new Stack<SpikeInvoker>();

        for (int i = 0; i < count; i++)
        {
            var randomIndex = Random.Range(0, list.Count);
            result.Push(list[randomIndex]);
            list.RemoveAt(randomIndex);
        }

        return result;
    }
}
