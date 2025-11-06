using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UpgradeMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private GameObject proposedUpgradeMenu;

    [SerializeField] private GameObject storedSkill;
    
    private int _storedIndex;
    
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

        foreach(Transform child in proposedUpgradeMenu.transform)
        {
            Destroy(child.gameObject);
        }

        if (SkillTreeManager.Instance.availableNode.Count >= 3)
        {
            ChooseSkill(3);
        }
        else
        {
            ChooseSkill(SkillTreeManager.Instance.availableNode.Count);
        }
    }

    private void ChooseSkill(int nb)
    {
        List<int> pickList = new List<int>();
        for (int i = 0; i < nb; i++)
        {
            var rndSkill = Random.Range(0, SkillTreeManager.Instance.availableNode.Count);
            while (pickList.Contains(rndSkill))
            {
                rndSkill = Random.Range(0, SkillTreeManager.Instance.availableNode.Count);
            }
            pickList.Add(rndSkill);
            var choosenSkill = SkillTreeManager.Instance.availableNode[rndSkill];
            GameObject newSkill = Instantiate(choosenSkill.SkillDisplay, transform);
            newSkill.transform.SetParent(transform, false);
            newSkill.transform.SetParent(proposedUpgradeMenu.transform);
            newSkill.transform.localScale = new Vector3(1,1,1);
            newSkill.GetComponent<ChooseUpdate>().index = rndSkill;
        }
    }

    public void HideUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void StoreSelectedSkill(GameObject effect, int index)
    {
        _storedIndex =  index;
        SkillTreeManager.Instance.availableNode[_storedIndex].IsSelected = true;
        storedSkill = effect;
    }
    
    public void ActivateSkill()
    {
        SkillTreeManager.Instance.availableNode[_storedIndex].IsSelected = false;
        SkillTreeManager.Instance.availableNode[_storedIndex].IsActive = true;
        var node = SkillTreeManager.Instance.availableNode[_storedIndex];
        var children = new List<int>();
        foreach (var child in node.Children)
        {
            children.Add(child);
        }
        storedSkill.GetComponent<SkillActivator>().ActivateSkill(node, children);
        //DisplayUpgradeMenu();
    }
}
