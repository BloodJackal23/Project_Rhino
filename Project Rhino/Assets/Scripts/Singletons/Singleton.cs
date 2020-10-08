using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }
    protected bool dontDestroyOnLoad = true; //True by default
    protected virtual void Awake()
    {
        Init(dontDestroyOnLoad);
    }

    void Init(bool _dontDestroyOnLoad)
    {
        Debug.LogWarning("An instance of the singleton " + typeof(T).ToString() + " has been created. " + "Dont destroy = " + _dontDestroyOnLoad);
        if (!instance)
        {
            instance = FindObjectOfType<T>();
            if(_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("More than one instance for object of type " + typeof(T).ToString() + " are present in this scene! Duplicates destroyed!" + "Dont destroy = " + _dontDestroyOnLoad);
        }
    }
}
