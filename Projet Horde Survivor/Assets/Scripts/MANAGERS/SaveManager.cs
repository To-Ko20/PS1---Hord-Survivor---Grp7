using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Inventory inventory =  new Inventory();
    
    public static SaveManager Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadFromJson();
    }
    
    public void SaveToJson()
    {
        string inventoryData = JsonUtility.ToJson(inventory);
        string filePath = Application.persistentDataPath + "/save.json";
        Debug.Log(filePath);
        
        System.IO.File.WriteAllText(filePath, inventoryData);
        Debug.Log("Sauvegarde effectuée");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/save.json";
        string inventoryData = System.IO.File.ReadAllText(filePath);
        
        inventory = JsonUtility.FromJson<Inventory>(inventoryData);
        Debug.Log("Chargement terminé");
    }
}

[System.Serializable]
public class Inventory
{
    public Dictionary<string, float> volumes =  new Dictionary<string, float>
    {
        {"Master", 0f},
        {"Music", 0f},
        {"SFX", 0f}
    };
}