using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>, IManager
{

    private const string saveName = "SaveExempleFile.txt";
    public void Save<T>(T saveData)
    {
        //save location 
        string saveLoc = Application.persistentDataPath + $"/{saveName}";
        Debug.Log(saveLoc);
        // if their no save data 
        if (!File.Exists(saveLoc))
        {
            // transform the savedata to Json
            string saveJson = JsonUtility.ToJson(saveData);
            File.WriteAllText(saveLoc, saveJson);

        }


    }

    public T Load<T>()
    {

        string saveLoc = Application.persistentDataPath + $"/{saveName}";
       

        if (File.Exists(saveLoc))
        {
            string jsonData = File.ReadAllText(saveLoc);
            return JsonUtility.FromJson<T>(jsonData);
        }
        return default;
    }

    public void Init()
    {
        throw new System.NotImplementedException();
    }

    public void Dispose()
    {
        throw new System.NotImplementedException();
    }
}
