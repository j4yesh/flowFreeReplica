using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dotStuff : MonoBehaviour
{
    public Vector3 offset;
    public Dictionary<Vector3, int> vectorMap = new Dictionary<Vector3, int>(new Vector3EqualityComparer());
     public float thickness = 0.2f;
    public List<Vector3> pointsList = new List<Vector3>();
    public LineRenderer lineRenderer;
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = thickness;
        lineRenderer.endWidth = thickness;
        lineRenderer.useWorldSpace = true;
        UpdateLineRenderer();
    }

    void Update()
    {
        //UpdateLineRenderer();
    }
    public void UpdateLineRenderer()
    {
        lineRenderer.positionCount = pointsList.Count;
        lineRenderer.SetPositions(pointsList.ToArray());
    }
}
