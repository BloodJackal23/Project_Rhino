using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Utility/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private SceneAsset levelScene;
    [SerializeField] private int levelIndex = 1;

    public SceneAsset LevelScene { get => levelScene; }
    public int LevelIndex { get => levelIndex; }
}
