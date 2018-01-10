﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Extenders;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    public Color Color = Color.green;
    public bool UseCollider = false;
    private Vector3 _frontTopLeft;
    private Vector3 _frontTopRight;
    private Vector3 _frontBottomLeft;
    private Vector3 _frontBottomRight;
    private Vector3 _backTopLeft;
    private Vector3 _backTopRight;
    private Vector3 _backBottomLeft;
    private Vector3 _backBottomRight;

    void Update()
    {
        CalcPositions();
        DrawBox();
    }

    void CalcPositions()
    {
        Bounds bounds = GetComponent<MeshFilter>().mesh.bounds;

        if (UseCollider)
        {
            BoxCollider bc = GetComponent<BoxCollider>();
            if (bc != null) bounds = bc.bounds;
            else return;
        }

        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;

        _frontTopLeft = new Vector3(center.x - extents.x, center.y + extents.y, center.z - extents.z).TransformPoint(transform);
        _frontTopRight = new Vector3(center.x + extents.x, center.y + extents.y, center.z - extents.z).TransformPoint(transform);
        _frontBottomLeft = new Vector3(center.x - extents.x, center.y - extents.y, center.z - extents.z).TransformPoint(transform);
        _frontBottomRight = new Vector3(center.x + extents.x, center.y - extents.y, center.z - extents.z).TransformPoint(transform);
        _backTopLeft = new Vector3(center.x - extents.x, center.y + extents.y, center.z + extents.z).TransformPoint(transform);
        _backTopRight = new Vector3(center.x + extents.x, center.y + extents.y, center.z + extents.z).TransformPoint(transform);
        _backBottomLeft = new Vector3(center.x - extents.x, center.y - extents.y, center.z + extents.z).TransformPoint(transform);
        _backBottomRight = new Vector3(center.x + extents.x, center.y - extents.y, center.z + extents.z).TransformPoint(transform);

        
    }

    void DrawBox()
    {
        
    }
}