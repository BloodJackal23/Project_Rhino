using UnityEngine;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] LoadingBar loadingBar;
    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }

    private void OnEnable()
    {
        StartScene();
    }

    private void OnDisable()
    {
        LoadingSystem.whileLoading -= loadingBar.UpdateBar;
        LoadingSystem.onLoadEnd -= loadingBar.ResetBar;
    }

    void StartScene()
    {
        GameManager gameManager = GameManager.instance;
        LoadingSystem.whileLoading += loadingBar.UpdateBar;
        LoadingSystem.onLoadEnd += loadingBar.ResetBar;
    }
}
