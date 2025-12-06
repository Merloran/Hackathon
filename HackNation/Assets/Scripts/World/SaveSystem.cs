using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class SaveSystem
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "bosses_save.json");

    public static void SaveBosses(List<BossData> activeBosses)
    {
        WorldData wrapper = new WorldData();

        foreach (var boss in activeBosses)
        {
            wrapper.bosses.Add(boss.ToSaveData());
        }

        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(SavePath, json);
    }

    public static List<BossSaveData> LoadBosses()
    {
        if (!File.Exists(SavePath))
        {
            return new List<BossSaveData>();
        }

        string json = File.ReadAllText(SavePath);
        WorldData wrapper = JsonUtility.FromJson<WorldData>(json);

        return wrapper != null ? wrapper.bosses : new List<BossSaveData>();
    }

    public static void DeleteSaveFile()
    {
        if (File.Exists(SavePath)) File.Delete(SavePath);
    }
}