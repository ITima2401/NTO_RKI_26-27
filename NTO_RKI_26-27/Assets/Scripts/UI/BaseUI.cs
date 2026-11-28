using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseUI : MonoBehaviour
{
    public void LoadScen(int id)
    {
        SceneManager.LoadScene(id);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void Unpause()
    {
        Time.timeScale = 1f;
    }
}
