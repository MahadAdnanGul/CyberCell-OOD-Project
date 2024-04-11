using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainGame
{
    public class SceneChanger : MonoBehaviour
    {
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
