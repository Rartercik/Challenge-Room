using UnityEngine;

namespace Game
{
    public class Arrow : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
            {
                player.StartDying();
                gameObject.SetActive(false);
            }
        }
    }
}
