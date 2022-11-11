using UnityEngine;

namespace Game
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private DestroyingObject _visualization;

        private void Start()
        {
            _visualization.StartDestroying(() => Destroy(gameObject), DestroyingType.Destroying);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.MakeImmortal();
                Destroy(gameObject);
            }
        }
    }
}