using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace Game
{
    public class ArrowsFactory : AttackInstanceFactory
    {
        [SerializeField] private Arrow _arrowPrefab;
        [SerializeField] private float _arrowMaxDeflection;
        [SerializeField] private float _arrowSpeed;
        [SerializeField] private float _instantiateDistance;
        [SerializeField] private int _attackReloadInSeconds;
        [SerializeField] private float zPosition;

        private IObjectPool<Arrow> _pool;
        private Transform _transform;
        private bool _canAttack = true;

        private void Start()
        {
            _pool = new ObjectPool<Arrow>(CreateArrow, DoOnGetActions, (arrow) => SetActivityTo(arrow, false));
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
                _pool.Get();
            }

            _canAttack = false;
            StartCoroutine(WaitForAttack(_attackReloadInSeconds));

        }

        private Arrow CreateArrow()
        {
            var arrow = Instantiate(_arrowPrefab);
            arrow.OnSwitchedOff += (arrow) => _pool.Release(arrow);

            return arrow;
        }

        private void DoOnGetActions(Arrow arrow)
        {
            SetActivityTo(arrow, true);
            var directionFromFactory = Random.insideUnitCircle.normalized;

            var arrowPosition = CalculateArrowPosition(_transform.position, directionFromFactory, _instantiateDistance);
            var arrowDeflection = Random.Range(-_arrowMaxDeflection, _arrowMaxDeflection);
            var arrowRotation = CalculateArrowRotation(-directionFromFactory, arrowDeflection);
            var arrowVelocity = Quaternion.Euler(0, 0, arrowDeflection) * -directionFromFactory.normalized * _arrowSpeed;

            arrow.Transform.position = arrowPosition;
            arrow.Transform.rotation = arrowRotation;
            arrow.Rigidbody.velocity = arrowVelocity;
        }

        private void SetActivityTo(Arrow arrow, bool active)
        {
            arrow.gameObject.SetActive(active);
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
}
