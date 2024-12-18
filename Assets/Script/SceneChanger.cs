using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        Debug.Log("sceneName=" + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
