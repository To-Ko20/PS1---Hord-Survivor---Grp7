using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SkillTreeViewerWindow : EditorWindow
{
    SkillTree targetTree;

    Vector2 pan = Vector2.zero;
    float zoom = 1f;

    private const float nodeWidth = 120f;
    private const float nodeHeight = 50f;
    private const float levelDistance = 150f;

    private Dictionary<int, NodePos> cachedPositions = new Dictionary<int, NodePos>();
    private List<Line> cachedLines = new List<Line>();
    private bool needsRecalc = true;

    [MenuItem("Window/Skill Tree Viewer")]
    public static void OpenWindow()
    {
        GetWindow<SkillTreeViewerWindow>("Skill Tree Viewer");
    }

    private void OnGUI()
    {
        // Toolbar
        GUILayout.BeginHorizontal(EditorStyles.toolbar);
        SkillTree newTarget = EditorGUILayout.ObjectField("SkillTree:", targetTree, typeof(SkillTree), true) as SkillTree;
        if (newTarget != targetTree)
        {
            targetTree = newTarget;
            needsRecalc = true;
        }
        if (GUILayout.Button("Refresh", EditorStyles.toolbarButton))
        {
            needsRecalc = true;
        }
        GUILayout.EndHorizontal();

        if (targetTree == null || targetTree.root == null || targetTree.root.Length == 0)
            return;

        Event e = Event.current;

        // Zoom
        if (e.type == EventType.ScrollWheel)
        {
            float oldZoom = zoom;
            zoom = Mathf.Clamp(zoom * (1f - e.delta.y * 0.01f), 0.2f, 3f);
            Vector2 mouse = e.mousePosition;
            pan += (mouse - pan) - (mouse - pan) * (zoom / oldZoom);
            needsRecalc = true;
            e.Use();
        }

        // Pan
        if (e.type == EventType.MouseDrag && e.button == 1)
        {
            pan += e.delta;
            needsRecalc = true;
            e.Use();
        }

        // Recalculate positions if needed
        if (needsRecalc)
        {
            cachedPositions.Clear();
            cachedLines.Clear();
            cachedPositions = CalculatePositions(targetTree, position.size / 2f + pan);
            foreach (var kv in cachedPositions)
            {
                Node n = kv.Value.node;
                Vector2 parentPos = kv.Value.position;
                foreach (int childIndex in n.Children)
                {
                    if (cachedPositions.ContainsKey(childIndex))
                    {
                        cachedLines.Add(new Line { start = parentPos, end = cachedPositions[childIndex].position });
                    }
                }
            }
            needsRecalc = false;
        }

        // Draw edges
        Handles.BeginGUI();
        Handles.color = Color.white;
        foreach (var line in cachedLines)
        {
            Handles.DrawLine(line.start, line.end);
        }
        Handles.EndGUI();

        // Draw nodes
        foreach (var kv in cachedPositions)
        {
            NodePos np = kv.Value;
            Rect rect = new Rect(np.position - new Vector2(nodeWidth, nodeHeight) * 0.5f, new Vector2(nodeWidth, nodeHeight));

            // Determine color
            Color color = Color.red;
            if (np.node.IsActive)
                color = Color.green;
            else
            {
                bool parentActive = false;
                for (int i = 0; i < targetTree.root.Length; i++)
                {
                    if (targetTree.root[i].Children.Contains(kv.Key))
                    {
                        if (targetTree.root[i].IsActive) parentActive = true;
                    }
                }
                if (parentActive) color = Color.blue;
                if (np.node.IsSelected)
                    color = new Color(1f, 0.49f, 0f);
            }

            EditorGUI.DrawRect(rect, color);
            GUIStyle style = new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.white }
            };
            GUI.Label(rect, $"{kv.Key}: {np.node.Name}", style);
        }
    }

    private Dictionary<int, NodePos> CalculatePositions(SkillTree tree, Vector2 center)
    {
        Dictionary<int, NodePos> positions = new Dictionary<int, NodePos>();

        void PlaceNode(int index, Vector2 pos, float angleStart, float angleEnd)
        {
            Node n = tree.root[index];
            positions[index] = new NodePos { node = n, position = pos };

            int childCount = n.Children.Count;
            if (childCount == 0) return;

            float angleStep = (angleEnd - angleStart) / childCount;
            float angle = angleStart;
            for (int i = 0; i < childCount; i++)
            {
                int childIndex = n.Children[i];
                float childAngle = angle + angleStep / 2f;
                Vector2 childPos = pos + new Vector2(Mathf.Cos(childAngle), Mathf.Sin(childAngle)) * levelDistance;
                PlaceNode(childIndex, childPos, angle, angle + angleStep);
                angle += angleStep;
            }
        }

        PlaceNode(0, center, -Mathf.PI / 2f, Mathf.PI * 3f / 2f);
        return positions;
    }

    private class NodePos
    {
        public Vector2 position;
        public Node node;
    }

    private class Line
    {
        public Vector2 start;
        public Vector2 end;
    }
}
