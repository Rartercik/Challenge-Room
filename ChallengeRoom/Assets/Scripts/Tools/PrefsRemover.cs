using UnityEngine;

namespace Tools
{
    public class PrefsRemover : MonoBehaviour
    {
        private void Start()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}