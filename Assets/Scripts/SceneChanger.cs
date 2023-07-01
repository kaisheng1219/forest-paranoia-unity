using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger Instance;

    private Animator animator;
    private string sceneName;

    void Start()
    {
        if (Instance != null)
        {
            Destroy(transform.root);
            return;
        }

        Instance = this;
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(transform.root);
    }

    public void SwitchScene(string sceneName)
    {
        this.sceneName = sceneName;
        animator.Play("FadeOut", -1, 0);
    }

    public void InitScene()
    {
        animator.Play("FadeIn", -1, 0);
    }

    public void OnFadeOutDone()
    {
        SceneManager.LoadScene(sceneName);
    }
}
