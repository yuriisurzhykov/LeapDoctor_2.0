using UnityEngine;
using UnityEngine.SceneManagement;

public class AdminLogicBut : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
