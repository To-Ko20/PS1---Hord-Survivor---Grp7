using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    [SerializeField] private SkillActivator sk; 
    public void Activate()
    {
        Debug.Log("Base Effect Start");
        SkillTreeManager.Instance.UpdateAvailableList(sk.node, sk.children);
        Destroy(gameObject);
    }
}
