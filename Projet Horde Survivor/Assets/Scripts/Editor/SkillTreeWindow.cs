/*using System.Collections.Generic;
using UnityEngine;

public class SkillTreeDebugOverlay : MonoBehaviour
{
    public SkillTree targetTree;
    public bool showOverlay = true;

    private Dictionary<int, Vector2> cachedPositions = new Dictionary<int, Vector2>();
    private List<(Vector2 start, Vector2 end)> cachedLines = new List<(Vector2, Vector2)>();
    private float levelDistance = 150f;

    private void OnGUI()
    {
        if (!showOverlay || targetTree == null || targetTree.nodeList == null) return;

        Vector2 center = new Vector2(Screen.width, Screen.height) / 2f;

        // Recalculate positions every frame (or cache if tree is huge)
        cachedPositions.Clear();
        cachedLines.Clear();
        CalculatePositions(targetTree, center);

        // Draw lines
        foreach (var line in cachedLines)
        {
            Drawing.DrawLine(line.start, line.end, Color.white, 2f);
        }

        // Draw nodes
        foreach (var kv in cachedPositions)
        {
            Node n = targetTree.nodeList[kv.Key];
            Rect rect = new Rect(kv.Value - new Vector2(60, 25), new Vector2(120, 50));
            Color color = n.IsActive ? Color.green : Color.red;
            Drawing.DrawRect(rect, color);
            GUI.Label(rect, $"{kv.Key}: {n.Name}", new GUIStyle()
            {
                alignment = TextAnchor.MiddleCenter,
                normal = new GUIStyleState() { textColor = Color.white }
            });
        }
    }

    private void CalculatePositions(SkillTree tree, Vector2 center)
    {
        void PlaceNode(int index, Vector2 pos, float angleStart, float angleEnd)
        {
            Node n = tree.nodeList[index];
            cachedPositions[index] = pos;

            int childCount = n.Children.Count;
            if (childCount == 0) return;

            float angleStep = (angleEnd - angleStart) / childCount;
            float angle = angleStart;
            for (int i = 0; i < childCount; i++)
            {
                int childIndex = n.Children[i];
                float childAngle = angle + angleStep / 2f;
                Vector2 childPos = pos + new Vector2(Mathf.Cos(childAngle), Mathf.Sin(childAngle)) * levelDistance;
                cachedLines.Add((pos, childPos));
                PlaceNode(childIndex, childPos, angle, angle + angleStep);
                angle += angleStep;
            }
        }

        PlaceNode(0, center, -Mathf.PI / 2f, Mathf.PI * 3f / 2f);
    }
}
*/