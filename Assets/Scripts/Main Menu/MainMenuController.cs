using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    
    private void Awake()
    {
        SceneChanger.Instance.InitScene();
    }

    public void StartGame()
    {
        SceneChanger.Instance.SwitchScene("Tutorial");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
