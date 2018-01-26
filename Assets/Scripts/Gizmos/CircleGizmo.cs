using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CircleGizmo : MonoBehaviour
{
    public int resolution = 10;
    public bool useOriginal = true;

    void OnDrawGizmosSelected()
    {
        if(useOriginal) Original();
        else MyTest();
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

    void MyTest()
    {
        //float step = 2f / resolution;
        //float step2 = 2f * step;
        //for (int i = 0; i <= resolution/2f; i++)
        //{
        //    ShowPoint(i * step2 - 1f, -1f);
        //    ShowPoint(1f - i * step2, 1f);
        //    //Debug.Log(string.Format("i {0}, x {1}, y {2}", i, i*step2 - 1f, 1f - i*step2));
        //}

        if (resolution%2 == 0)
        {
            var res = resolution/4;
            float step = 2f/res;
            for (int i = 0; i <= res; i++)
            {
                ShowPoint(i * step - 1f, -1f);
                ShowPoint(i * step - 1f, 1f);
            }
            for (int i = 0; i < res; i++)
            {
                ShowPoint(-1f, i * step - 1f);
                ShowPoint(1f, i * step - 1f);
            }
        }
        else
        {
            ShowPoint(0f, 1f);
            ShowPoint(-1f, -2f/3);
            ShowPoint(1f, -2f/3);
        }
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