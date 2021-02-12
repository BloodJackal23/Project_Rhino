using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private static SaveFile[] saveFiles;
    private static string PATH_DIRECTORY = Application.persistentDataPath + "/Saves";
    private static string path = Application.persistentDataPath + "/Saves/savefile.kmp";
    public static string Path { get => path; }

    public static void SaveGame(GameLevel[] _unlockedLevels)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        Debug.Log(path);
        FileStream fileStream = new FileStream(path, FileMode.Create);
        SaveFile saveFile = new SaveFile(_unlockedLevels);
        formatter.Serialize(fileStream, saveFile);
        fileStream.Close();
    }

    public static void CreateSaveFolder()
    {
        if(!Directory.Exists(PATH_DIRECTORY))
        {
            Directory.CreateDirectory(PATH_DIRECTORY);
        }
    }

    public static SaveFile LoadFile(string _path)
    {
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(_path, FileMode.Open);
            SaveFile loadedFile = (SaveFile)formatter.Deserialize(fileStream);
            fileStream.Close();
            return loadedFile;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
