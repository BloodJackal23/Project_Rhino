using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] LoadingBar loadingBar;
    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }

    private void Start()
    {
        StartScene();
    }

    IEnumerator LoadNextScene(AsyncOperation _operation)
    {
        float progress = 0;
        while (!_operation.isDone)
        {
            progress = Mathf.Clamp01(_operation.progress / .9f);
            loadingBar.UpdateBar(progress);
            yield return null;
        }
        loadingBar.ResetBar();
    }

    void StartScene()
    {
        GameManager gameManager = GameManager.instance;
        AsyncOperation operation = SceneManager.LoadSceneAsync(gameManager.nextScene);
        StartCoroutine(LoadNextScene(operation));
    }
}
