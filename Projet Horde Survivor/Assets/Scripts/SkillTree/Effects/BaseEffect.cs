using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(gameObject.GetComponent<SkillActivator>().effectActivated);
        if (gameObject.GetComponent<SkillActivator>().effectActivated)
        {
            Debug.Log("activate");
            Activate();
        }
    }

    private void Activate()
    {
        Debug.Log("Base Effect Start");
        SkillTreeManager.Instance.UpdateAvailableList(gameObject.GetComponent<SkillActivator>().node, gameObject.GetComponent<SkillActivator>().children);
        Destroy(gameObject);
    }
}
