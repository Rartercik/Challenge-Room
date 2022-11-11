using UnityEditor;
using UnityEngine;

namespace Game
{
    public class ShieldsFactory : MonoBehaviour
    {
        [SerializeField] private Shield _shieldPrefab;
        [SerializeField] private Transform _shieldParent;
        [SerializeField] private float _creationRange;
        [SerializeField] private int _minCreationInterval;
        [SerializeField] private int _maxCreationInterval;

        private int _creationInterval;
        private int _creationProgress;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.blue;

            Handles.DrawWireDisc(transform.position, Vector3.forward, _creationRange);
        }
#endif

        private void Start()
        {
            _creationInterval = GetRandomInterval();
        }

        public void UpdateFactory()
        {
            _creationProgress++;

            if (_creationProgress == _creationInterval)
            {
                _creationInterval = GetRandomInterval();
                _creationProgress = 0;
                CreateShield();
            }
        }

        private void CreateShield()
        {
            var position = GetRandomPosition(_creationRange);

            Instantiate(_shieldPrefab, position, Quaternion.identity, _shieldParent);
        }

        private int GetRandomInterval()
        {
            return Random.Range(_minCreationInterval, _maxCreationInterval);
        }

        private Vector3 GetRandomPosition(float creationRange)
        {
            var remoteness = Random.Range(0, creationRange);
            var vectorFromCenter = Vector3.up * remoteness;

            var randomAngle = Random.Range(0, 360);
            var vectorAngle = Quaternion.Euler(0, 0, randomAngle);

            return vectorAngle * vectorFromCenter;
        }
    }
}