using System.Collections;
using UnityEngine;

public class ArrowsFactory : AttackInstanceFactory
{
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] float _arrowMaxDeflection;
    [SerializeField] float _arrowSpeed;
    [SerializeField] float _instantiateDistance;
    [SerializeField] int _attackReloadInSeconds;
    [SerializeField] float zPosition;

    private Transform _transform;
    private bool _canAttack = true;

    private void Start()
    {
        _transform = transform;
    }

    protected override bool GetAttackAvailable()
    {
        return _canAttack;
    }

    protected override void Attack(int attacksCount)
    {
        for (int i = 0; i < attacksCount; i++)
        {
            CreateArrow();
        }

        _canAttack = false;
        StartCoroutine(WaitForAttack(_attackReloadInSeconds));
        
    }

    private void CreateArrow()
    {
        var directionFromFactory = Random.insideUnitCircle.normalized;

        var arrowPosition = CalculateArrowPosition(_transform.position, directionFromFactory, _instantiateDistance);

        var arrowDeflection = Random.Range(-_arrowMaxDeflection, _arrowMaxDeflection);
        var arrowRotation = CalculateArrowRotation(-directionFromFactory, arrowDeflection);

        var arrow = Instantiate(_arrowPrefab, arrowPosition, arrowRotation);
        var arrowVelocity = Quaternion.Euler(0, 0, arrowDeflection) * -directionFromFactory.normalized * _arrowSpeed;
        arrow.GetComponent<Rigidbody2D>().velocity = arrowVelocity;
    }

    private Vector3 CalculateArrowPosition(Vector2 factoryPosition, Vector2 directionFromFactory, float instantiateDistance)
    {
        var arrow2DPosition = factoryPosition + directionFromFactory * instantiateDistance;
        return new Vector3(arrow2DPosition.x, arrow2DPosition.y, zPosition);
    }

    private Quaternion CalculateArrowRotation(Vector2 directionToCenter, float arrowDeflection)
    {
        var arrowRotationToCenter = Quaternion.LookRotation(Vector3.forward,
                                    new Vector3(directionToCenter.x, directionToCenter.y, 0));

        return arrowRotationToCenter * Quaternion.Euler(0, 0, arrowDeflection);
    }

    private IEnumerator WaitForAttack(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        _canAttack = true;
    }
}
