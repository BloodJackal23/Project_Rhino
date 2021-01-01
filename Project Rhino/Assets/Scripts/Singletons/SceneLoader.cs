using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] LoadingBar loadingBar;
    public Scene targetScene;
    protected override void Awake()
    {
        dontDestroyOnLoad = false;
        base.Awake();
    }

    private void Start()
    {
        GameManager gameManager = GameManager.instance;
    }
}
