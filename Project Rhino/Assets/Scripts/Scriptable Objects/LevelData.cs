using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Utility/Level Data")]
public class LevelData : ScriptableObject
{
    [SerializeField] private SceneSystem.GameScene levelScene;
    [SerializeField] private int levelIndex = 1;

    public SceneSystem.GameScene LevelScene { get => levelScene; }
    public int LevelIndex { get => levelIndex; }
}
