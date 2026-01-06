using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    public Inventory inventory = new Inventory();

    private string _filePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _filePath = Application.persistentDataPath + "/save.json";
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
        ClampVolumes();

        string json = JsonUtility.ToJson(inventory, true);
        System.IO.File.WriteAllText(_filePath, json);
    }

    private void ClampVolumes()
    {
        var v = inventory.volumes;

        v.Master = Mathf.Clamp01(v.Master);
        v.Music  = Mathf.Clamp01(v.Music);
        v.SFX    = Mathf.Clamp01(v.SFX);
    }

    public void LoadFromJson()
    {
        if (!File.Exists(_filePath))
        {
            Debug.Log("No save file found, using default values");
            return;
        }

        string json = File.ReadAllText(_filePath);
        inventory = JsonUtility.FromJson<Inventory>(json);
        Debug.Log("Load complete");
    }
}

[System.Serializable]
public class Inventory
{
    public VolumeData volumes = new VolumeData();
}

[System.Serializable]
public class VolumeData
{
    [Range(0f, 1f)] public float Master = 1f;
    [Range(0f, 1f)] public float Music = 1f;
    [Range(0f, 1f)] public float SFX   = 1f;
}