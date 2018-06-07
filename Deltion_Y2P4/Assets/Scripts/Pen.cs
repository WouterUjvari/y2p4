using UnityEditor.Presets;
using UnityEngine;

public class Pen : MonoBehaviour
{

    private bool canDraw;
    private bool isDrawing;
    private LineRenderer currentLine;
    private int currentPositionIndex;
    private Transform currentDrawingSurface;
    private Vector3 penHitPoint;

    [SerializeField]
    private Transform penPoint;
    [SerializeField]
    private Preset lineRendererPreset;
    [SerializeField]
    private float distanceBetweenDrawPoints = 0.01f;
    [SerializeField]
    private LayerMask drawLayerMask;
    [SerializeField]
    private float drawRayLength = 0.05f;

    private void Update()
    {
        Debug.DrawRay(penPoint.transform.position, penPoint.transform.forward * drawRayLength, Color.red);

        if (!canDraw)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(penPoint.transform.position, penPoint.transform.forward, out hit, drawRayLength, drawLayerMask))
        {
            penHitPoint = hit.point;

            if (!isDrawing)
            {
                CreateNewLine(hit.transform);
                currentDrawingSurface = hit.transform;
            }
            else
            {
                if (hit.transform != currentDrawingSurface)
                {
                    EndLine();
                }
            }
        }
        else
        {
            EndLine();
        }

        if (!isDrawing)
        {
            return;
        }

        if (Vector3.Distance(penHitPoint, currentLine.transform.TransformPoint(currentLine.GetPosition(currentPositionIndex - 1))) > distanceBetweenDrawPoints)
        {
            AddPenPoint();
        }
    }

    private void CreateNewLine(Transform surface)
    {
        GameObject newLine = new GameObject();
        newLine.transform.SetParent(surface);
        newLine.transform.position = penPoint.position;
        currentLine = newLine.AddComponent<LineRenderer>();
        lineRendererPreset.ApplyTo(currentLine);
        currentPositionIndex = 0;

        AddPenPoint();
        isDrawing = true;
    }

    private void AddPenPoint()
    {
        currentLine.positionCount = currentPositionIndex + 1;
        currentLine.SetPosition(currentPositionIndex, currentLine.transform.InverseTransformPoint(penHitPoint));
        currentPositionIndex++;
    }

    private void EndLine()
    {
        currentLine = null;
        isDrawing = false;
    }

    public void GrabPenEvent(bool canDraw)
    {
        this.canDraw = canDraw;
    }
}
