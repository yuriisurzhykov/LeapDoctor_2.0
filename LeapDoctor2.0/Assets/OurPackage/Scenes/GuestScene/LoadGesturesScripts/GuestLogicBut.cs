using UnityEngine.SceneManagement;
using UnityEngine;

public class GuestLogicBut : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
