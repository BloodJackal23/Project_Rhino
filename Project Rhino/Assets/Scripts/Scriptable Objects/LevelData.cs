using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Utility/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int levelIndex = 1;
    public int LevelIndex { get => levelIndex; }
}
