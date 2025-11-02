using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenu;
    
    [SerializeField] private List<UpgradesTree> upgradesTree;
    [SerializeField] private GameObject proposedUpgradeMenu;
    
    public static UpgradeMenuManager Instance;
    
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

    public void DisplayUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
        Time.timeScale = 0;

        for (int i = 0; i < 3; i++)
        {
            var choosenTree =  upgradesTree[Random.Range(0, upgradesTree.Count)];
            var choosenUpgrade = choosenTree.upgradesList[Random.Range(0, choosenTree.upgradesList.Count)];
            GameObject newUpgrade = Instantiate(choosenUpgrade, transform);
            newUpgrade.transform.SetParent(transform, false);
            newUpgrade.transform.SetParent(proposedUpgradeMenu.transform);
            newUpgrade.transform.localScale = new Vector3(1,1,1);
        }
    }
}

[System.Serializable]
internal class UpgradesTree
{
    public List<GameObject> upgradesList;
}
