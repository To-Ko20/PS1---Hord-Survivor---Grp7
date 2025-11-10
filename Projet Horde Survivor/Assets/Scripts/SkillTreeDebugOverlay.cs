/*using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteAlways]
[InitializeOnLoad]
public class SkillTreeDebugOverlay : MonoBehaviour
{
     public SkillTree targetTree;
    public bool showOverlay = true;
    public float nodeWidth = 120f;
    public float nodeHeight = 50f;
    public float levelDistance = 150f;

    private Dictionary<int, Vector2> cachedPositions = new Dictionary<int, Vector2>();
    private List<(Vector2 start, Vector2 end)> cachedLines = new List<(Vector2 start, Vector2 end)>();
    private bool needsRecalc = true;

    static SkillTreeDebugOverlay()
    {
        // Automatically attach Scene GUI drawing
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnValidate()
    {
        needsRecalc = true;
        SceneView.RepaintAll();
    }

    private void Update()
    {
        if (!showOverlay || targetTree == null) return;

        // Always update if something changed (runtime or edit)
        if (needsRecalc)
        {
            RecalculateGraph();
            needsRecalc = false;
        }

        // Force SceneView to repaint in edit mode too
        SceneView.RepaintAll();
    }

    private void RecalculateGraph()
    {
        cachedPositions.Clear();
        cachedLines.Clear();

        if (targetTree == null || targetTree.nodeList == null || targetTree.nodeList.Length == 0)
            return;

        Vector2 center = Vector2.zero;
        PlaceNodeRecursive(0, center, -Mathf.PI / 2f, Mathf.PI * 3f / 2f);
    }

    private void PlaceNodeRecursive(int index, Vector2 pos, float angleStart, float angleEnd)
    {
        if (index < 0 || index >= targetTree.nodeList.Length) return;

        SkillNode n = targetTree.nodeList[index];
        cachedPositions[index] = pos;

        int childCount = n.Children.Count;
        if (childCount == 0) return;

        float angleStep = (angleEnd - angleStart) / childCount;
        float angle = angleStart;
        for (int i = 0; i < childCount; i++)
        {
            int childIndex = n.Children[i];
            if (childIndex < 0 || childIndex >= targetTree.nodeList.Length)
                continue;

            float childAngle = angle + angleStep / 2f;
            Vector2 childPos = pos + new Vector2(Mathf.Cos(childAngle), Mathf.Sin(childAngle)) * levelDistance;

            cachedLines.Add((pos, childPos));
            PlaceNodeRecursive(childIndex, childPos, angle, angle + angleStep);
            angle += angleStep;
        }
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        foreach (var debugger in Object.FindObjectsByType<SkillTreeDebugOverlay>(
                     FindObjectsInactive.Include,
                     FindObjectsSortMode.None))
        {
            if (!debugger.showOverlay) continue;
            debugger.DrawGraphInSceneView();
        }
    }

    private void DrawGraphInSceneView()
    {
        if (targetTree == null || targetTree.nodeList == null || targetTree.nodeList.Length == 0)
            return;

        Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;

        // Draw lines
        Handles.color = Color.white;
        foreach (var line in cachedLines)
        {
            Handles.DrawLine(new Vector3(line.start.x, line.start.y, 0),
                             new Vector3(line.end.x, line.end.y, 0));
        }

        // Draw nodes
        foreach (var kv in cachedPositions)
        {
            SkillNode n = targetTree.nodeList[kv.Key];
            Vector3 pos = new Vector3(kv.Value.x, kv.Value.y, 0);

            Color color = GetNodeColor(kv.Key, n);
            Handles.DrawSolidRectangleWithOutline(
                new Rect(pos - new Vector3(nodeWidth / 2, nodeHeight / 2, 0),
                         new Vector2(nodeWidth, nodeHeight)),
                color,
                Color.black
            );

            GUIStyle style = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white },
                fontSize = 12
            };

            Handles.Label(pos + new Vector3(0, 0, 0), $"{kv.Key}: {n.Name}", style);
        }
    }

    private Color GetNodeColor(int index, SkillNode n)
    {
        if (n.IsActive) return Color.green;

        bool parentActive = false;
        bool parentSelected = false;
        foreach (var parent in targetTree.nodeList)
        {
            if (parent.Children.Contains(index))
            {
                if (parent.IsActive) parentActive = true;
                if (parent.IsSelected) parentSelected = true;
            }
        }

        if (parentSelected) return new Color(0f, 0.2f, 0.6f); // dark blue
        if (parentActive) return Color.blue;
        if (n.IsSelected) return new Color(1f, 0.49f, 0f); // orange
        return Color.red;
    }
}
*/