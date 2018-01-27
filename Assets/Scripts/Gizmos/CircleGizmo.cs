using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CircleGizmo : MonoBehaviour
{
    public int resolution = 10;
    public GenerationType Generator = GenerationType.UseOriginal;

    public enum GenerationType
    {
        UseOriginal,
        UseMyCircle
    }

    private Dictionary<GenerationType, Action> generatorSelection;

    public CircleGizmo()
    {
        generatorSelection = new Dictionary<GenerationType, Action>
        {
            { GenerationType.UseOriginal, Original },
            { GenerationType.UseMyCircle, MyCircle }
        };
    }

    void OnDrawGizmosSelected()
    {
        generatorSelection[Generator]();
    }

    void Original()
    {
        float step = 2f / resolution;
        for (int i = 0; i <= resolution; i++)
        {
            ShowPoint(i * step - 1f, -1f);
            ShowPoint(i * step - 1f, 1f);
        }
        for (int i = 0; i < resolution; i++)
        {
            ShowPoint(-1f, i * step - 1f);
            ShowPoint(1f, i * step - 1f);
        }
    }

    void MyCircle()
    {
        float step = 8f / resolution;
        for (int i = 0; i < resolution; i++)
        {
            var it = i*step;
            var ind = i + 1;
            if (it <= 1) DebugShowPoint(i, it, 1f);
            if (it > 1 && it <= 3) DebugShowPoint(ind, 1f, 2f - it);
            if (it > 3 && it <= 5) DebugShowPoint(ind, 4f - it, -1f);
            if (resolution%2 == 0)
            {
                if (it > 5 && it < 7) DebugShowPoint(ind, -1f, 6f - it);
            }
            else
            {
                if (it > 5 && it < 7) DebugShowPoint(ind, -1f, it - 6f);
            }
            if (it >= 7) DebugShowPoint(ind, it - 8f, 1f);
        }
    }

    void DebugShowPoint(int i, float x, float y)
    {
        //Debug.Log(string.Format("index {0}, x {1}, y {2}", i, x, y));
        ShowPoint(x,y);
    }

    void ShowPoint(float x, float y)
    {
        Vector2 square = new Vector2(x, y);
        Vector2 circle;
        circle.x = square.x*Mathf.Sqrt(1f - square.y*square.y*0.5f);
        circle.y = square.y*Mathf.Sqrt(1f - square.x*square.x*0.5f);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(square, 0.025f);

        Gizmos.color = Color.white;
        Gizmos.DrawSphere(circle, 0.025f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(square, circle);

        Gizmos.color = Color.gray;
        Gizmos.DrawLine(circle, Vector2.zero);
    }
}