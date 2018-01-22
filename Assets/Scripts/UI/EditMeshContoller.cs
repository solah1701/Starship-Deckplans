using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Utilities;
using UnityEngine;

[RequireComponent(typeof (TouchHelper))]
public class EditMeshContoller : MonoBehaviour
{
    public ObjectModelManager ObjectModelManager;

    private TouchHelper _touchHelper;
    private float _startX, _startY;
    private float _scaleX, _scaleY;

    // Use this for initialization
    void Start()
    {
        _touchHelper = GetComponent<TouchHelper>();
        _touchHelper.CalculateScale();
        _scaleX = _touchHelper.GetScaleX();
        _scaleY = _touchHelper.GetScaleY();
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectModelManager.EditVertexMode)
            MoveVertices();
    }

    void MoveVertices()
    {
        if (!_touchHelper.TouchInBounds()) return;
        var touch = _touchHelper.GetTouch();
        var position = touch.deltaPosition;
        var dx = ObjectModelManager.LockXMode ? 0 : -position.x/_scaleX;
        var dy = ObjectModelManager.LockYMode ? 0 : -position.y/_scaleY;
        if (ObjectModelManager.SelectedVertices.Count < 1) return;
        var parent = ObjectModelManager.SelectedVertices[0].GetComponentInParent<MeshObject>();
        Debug.Log(string.Format("parent name {0}", parent.name));
        var mesh = parent.GetComponent<MeshFilter>().mesh;
        var meshVertices = mesh.vertices;
        foreach (GameObject selectedVertex in ObjectModelManager.SelectedVertices)
        {
            var index = meshVertices.IndexOf(v => v.Equals(selectedVertex.transform.position));
            selectedVertex.transform.Translate(dx, 0, dy);
            meshVertices[index] = selectedVertex.transform.position;
        }
        mesh.vertices = meshVertices;
        mesh.RecalculateBounds();
        //if (touch.phase == TouchPhase.Began)
        //{
        //    var startPosition = touch.position;
        //    _startX = _touchHelper.CalculatePosition(startPosition.x, Screen.width, Screen.width / _scaleX);
        //    _startY = _touchHelper.CalculatePosition(startPosition.y, Screen.height, Screen.height / _scaleY);

        //}
        //if (touch.phase == TouchPhase.Moved)
        //{
        //    var diff = touch.position;
        //    var px = _touchHelper.CalculatePosition(diff.x, Screen.width, Screen.width / _scaleX);
        //    var py = _touchHelper.CalculatePosition(diff.y, Screen.height, Screen.height / _scaleY);
        //    var diffX = _startX - px;
        //    var diffY = _startY - py;


        //}
        Debug.Log(string.Format("MoveVertices {0}", ObjectModelManager.SelectedVertices));
    }
}
