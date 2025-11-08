using System.Collections.Generic;
using UnityEngine;

public class SkillActivator : MonoBehaviour
{
    public Node node;
    public List<int> children = new List<int>();

    // ReSharper disable Unity.PerformanceAnalysis
    public void ActivateSkill(Node Node, List<int> Children)
    {
        node = Node;
        children = Children;
        gameObject.SendMessage("Activate");
    }
}
