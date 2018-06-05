using UnityEditor.Presets;
using UnityEngine;

public class Pen : MonoBehaviour
{

    private bool canDraw;
    private bool isDrawing;
    private LineRenderer currentLine;
    private int currentPositionIndex;
    private Collider currentDrawingSurface;

    [SerializeField]
    private Transform penPoint;
    [SerializeField]
    private Preset lineRendererPreset;
    [SerializeField]
    private float distanceBetweenDrawPoints = 0.01f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GrabPenEvent(true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            GrabPenEvent(false);
        }

        if (!isDrawing)
        {
            return;
        }

        if (Vector3.Distance(penPoint.position, currentLine.GetPosition(currentPositionIndex - 1)) > distanceBetweenDrawPoints)
        {
            AddPenPoint();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDraw)
        {
            return;
        }

        if (other.GetComponent<DrawableSurface>() != null)
        {
            if (other != currentDrawingSurface)
            {
                EndLine();
            }

            CreateNewLine(other.transform);
            currentDrawingSurface = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isDrawing)
        {
            if (other.transform == currentDrawingSurface)
            {
                EndLine();
            }
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
        currentLine.SetPosition(currentPositionIndex, currentLine.transform.InverseTransformPoint(penPoint.position));
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
