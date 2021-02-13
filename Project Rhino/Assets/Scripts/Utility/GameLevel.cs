using System;
using UnityEngine;

[Serializable]
public class GameLevel
{
    private SceneSystem.GameScene scene;
    private int leveIndex = 1;
    private bool isUnlocked = false;
    public SceneSystem.GameScene Scene { get => scene; }
    public int LevelIndex { get => leveIndex; }
    public bool IsUnlocked { get => isUnlocked; }

    public GameLevel(SceneSystem.GameScene _scene, int _levelIndex, bool _isUnlocked)
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
