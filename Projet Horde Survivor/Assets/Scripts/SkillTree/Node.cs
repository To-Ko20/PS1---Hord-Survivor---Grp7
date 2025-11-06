using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Node
{
    public string Name;

    public GameObject SkillDisplay;
    
    public bool IsSelected;
    public bool IsActive;

    public List<int> Children = new List<int>();
}
