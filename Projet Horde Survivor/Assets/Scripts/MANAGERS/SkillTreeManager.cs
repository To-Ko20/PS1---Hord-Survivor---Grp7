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
        for (int i = 1; i < 7; i++)
        {
            int node = skillsTree.nodeList[i].Children[0];
            if (skillsTree.nodeList[node].IsImplemented)
            {
                availableNode.Add(skillsTree.nodeList[node]);
            }
        }
    }

    public void UpdateAvailableList(SkillNode parent, List<int> children)
    {
        foreach (int node in children)
        {
            if (skillsTree.nodeList[node].HasTwoParents)
            {
                skillsTree.nodeList[node].HasTwoParents = false;
            }

            if (skillsTree.nodeList[node].IsImplemented)
            {
                availableNode.Add(skillsTree.nodeList[node]);
            }
        }
        availableNode.Remove(parent);
    }
}