using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SkillTree))]
public class SkillTreeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SkillTree tree = (SkillTree)target;

        if (GUILayout.Button("Add Root Node"))
        {
            tree.root = new Node { Name = "Root Node" };
            EditorUtility.SetDirty(tree);
        }

        if (tree.root != null && GUILayout.Button("Add Child Node"))
        {
            if (tree.root.Children == null)
                tree.root.Children = new List<Node>();
            tree.root.Children.Add(new Node { Name = "Child " + tree.root.Children.Count });
            EditorUtility.SetDirty(tree);
        }
    }
}