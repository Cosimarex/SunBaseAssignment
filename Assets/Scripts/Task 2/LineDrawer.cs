using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LineDrawer : MonoBehaviour
{
    private bool isDrawing = false;
    private LineRenderer LR;
    private List<Vector3> points = new List<Vector3>();
    private Camera Cam;

    void Start()
    {
        Cam = Camera.main;
        LR = GetComponent<LineRenderer>();
        LR.positionCount = 0;
    }

    void Update()
    {
        // Start drawing on mouse down
        if (Input.GetMouseButtonDown(0))
        {
            StartLine();
        }
        // End drawing on mouse up
        if (Input.GetMouseButtonUp(0))
        {
            EndLine();
        }

        // Update line while drawing
        if (isDrawing)
        {
            UpdateLine();
        }
    }

    void StartLine()
    {
        isDrawing = true;
        points.Clear(); // Reset points for a new line
        AddPoint();
    }

    void UpdateLine()
    {
        AddPoint();
        CheckIntersections();
    }

    void EndLine()
    {
        isDrawing = false;
    }

    void AddPoint()
    {
        Vector3 mousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        // Add the point only if it’s far
        if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], mousePos) > 0.1f)
        {
            points.Add(mousePos);
            LR.positionCount = points.Count;
            LR.SetPosition(points.Count - 1, mousePos);
        }
    }

    void CheckIntersections()
    {
        var circles = GameObject.FindGameObjectsWithTag("Circle");
        foreach (var circle in circles)
        {
            CircleCollider2D circleCollider = circle.GetComponent<CircleCollider2D>();
            if (circleCollider != null && DoesLineIntersectCircle(circleCollider))
            {
                circle.gameObject.transform.DOScale(Vector3.zero, 0.1f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    Destroy(circle); // Destroy after animation completes
                });
            }
        }
    }

    bool DoesLineIntersectCircle(CircleCollider2D circleCollider)
    {
        // Get the circle's center and radius
        Vector3 circleCenter = circleCollider.bounds.center;
        float radius = circleCollider.bounds.extents.x;

        // Check the last segment only
        if (points.Count < 2)
        {
            return false;
        }

        Vector3 start = points[points.Count - 2];
        Vector3 end = points[points.Count - 1];

        return LineIntersectsCircle(start, end, circleCenter, radius);
    }

    bool LineIntersectsCircle(Vector3 start, Vector3 end, Vector3 circleCenter, float radius)
    {
        // Vector math to calculate closest point on line to circle
        Vector3 d = end - start;
        Vector3 f = start - circleCenter;

        float a = Vector3.Dot(d, d);
        float b = 2 * Vector3.Dot(f, d);
        float c = Vector3.Dot(f, f) - radius * radius;

        float discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
        {
            return false; // No intersection
        }

        discriminant = Mathf.Sqrt(discriminant);

        // Check intersection points within segment
        float t1 = (-b - discriminant) / (2 * a);
        float t2 = (-b + discriminant) / (2 * a);

        return (t1 >= 0 && t1 <= 1) || (t2 >= 0 && t2 <= 1);
    }
}
