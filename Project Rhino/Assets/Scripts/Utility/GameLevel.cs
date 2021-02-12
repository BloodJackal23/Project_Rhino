using System;
using UnityEngine;

[Serializable]
public class GameLevel
{
    [SerializeField] private int leveIndex = 1;
    [SerializeField] private bool isUnlocked = false;
    public int LevelIndex { get => leveIndex; }
    public bool IsUnlocked { get => isUnlocked; }

    public GameLevel(int _levelIndex, bool _isUnlocked)
    {
        leveIndex = _levelIndex;
        isUnlocked = _isUnlocked;
    }

    public void SetLevelLock(bool _value)
    {
        isUnlocked = !_value;
    }
}
