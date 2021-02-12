using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class GameLevel
{
    private SceneAsset scene;
    private int leveIndex = 1;
    private bool isUnlocked = false;
    public SceneAsset Scene { get => scene; }
    public int LevelIndex { get => leveIndex; }
    public bool IsUnlocked { get => isUnlocked; }

    public GameLevel(SceneAsset _scene, int _levelIndex, bool _isUnlocked)
    {
        scene = _scene;
        leveIndex = _levelIndex;
        isUnlocked = _isUnlocked;
    }

    public void SetLevelLock(bool _value)
    {
        isUnlocked = !_value;
    }
}
