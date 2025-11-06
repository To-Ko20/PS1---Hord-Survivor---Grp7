using System.Collections.Generic;
using UnityEngine;

public class SkillActivator : MonoBehaviour
{
    public bool effectActivated = false;
    public Node node;
    public List<int> children = new List<int>();

    public void ActivateSkill(Node Node, List<int> Children)
    {
        node = Node;
        children = Children;
        effectActivated = true;
    }
}
