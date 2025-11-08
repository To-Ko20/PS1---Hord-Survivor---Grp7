using System.Collections.Generic;
using UnityEngine;

public class SkillActivator : MonoBehaviour
{
    public SkillNode node;
    public List<int> children = new List<int>();

    // ReSharper disable Unity.PerformanceAnalysis
    public void ActivateSkill(SkillNode Node, List<int> Children)
    {
        node = Node;
        children = Children;
        gameObject.SendMessage("Activate");
    }
}
