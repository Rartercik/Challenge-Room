using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameEnding : MonoBehaviour
    {
        public void RestartScene()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
