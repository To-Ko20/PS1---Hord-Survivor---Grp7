using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    [SerializeField] SkillTree skillsTree;
    public List<SkillNode> availableNode = new List<SkillNode>();
    
    public static SkillTreeManager Instance;

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
        for (int i = 1; i < 5; i++)
        {
            availableNode.Add(skillsTree.nodeList[i]);
        }
    }

    public void UpdateAvailableList(SkillNode parent, List<int> children)
    {
        foreach (int node in children)
        {
            availableNode.Add(skillsTree.nodeList[node]);
        }
        availableNode.Remove(parent);
        Debug.Log(availableNode.Count);
    }
}