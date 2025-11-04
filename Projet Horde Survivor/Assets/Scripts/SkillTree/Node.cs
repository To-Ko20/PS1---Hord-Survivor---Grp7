using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public string Name;
    
    [SerializeReference]
    public MonoBehaviour Effect;
    
    public bool IsSelected;
    public bool IsActive;
    
    [SerializeReference]
    public List<Node> Children;
}
