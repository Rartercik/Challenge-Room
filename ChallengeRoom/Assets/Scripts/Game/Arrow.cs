using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private Transform _effectTransform;
        [SerializeField] private float _timeToSwitchOff;

        [Space(30)]
        [Header("Required Components:")]
        [Space(5)]
        [SerializeField] private Rigidbody2D _rigidbody;

        [Button]
        private void SetRequiredComponents()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public Rigidbody2D Rigidbody => _rigidbody;

        private void Start()
        {
            StartCoroutine(SwitchOffAfter(_timeToSwitchOff));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                player.TryApplyDamage();
                _effectTransform.SetParent(null);
                _effect.Stop();
                gameObject.SetActive(false);
            }
        }

        private IEnumerator SwitchOffAfter(float seconds)
        {
            yield return new WaitForSeconds(seconds);

            gameObject.SetActive(false);
        }
    }
}
