using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Inventory inventory =  new Inventory();
    
    public void SaveToJson()
    {
        SetInventory();
        
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
        FillInventory();
        ClickerManager.Instance.DisplayUpdate();
        Debug.Log("Chargement terminé");
    }
    
    private void FillInventory()
    {
        ClickerManager.Instance.bits = inventory.bits;
        ClickerManager.Instance.clicks = inventory.clicks;
    }
    
    private void SetInventory()
    {
        inventory.bits = ClickerManager.Instance.bits;
        inventory.clicks = ClickerManager.Instance.clicks;
    }
}

[System.Serializable]
public class Inventory
{
    public ulong bits;
    public int clicks;
}