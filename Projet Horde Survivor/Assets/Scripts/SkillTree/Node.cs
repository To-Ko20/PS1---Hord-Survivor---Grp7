using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillNode
{
    public string Name;

    public GameObject SkillDisplay;
    
    public bool IsSelected;
    public bool IsImplemented;
    public bool HasTwoParents;
    public bool IsActive;

    public List<int> Children = new List<int>();
}
