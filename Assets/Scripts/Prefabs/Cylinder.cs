using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Extenders;
using Assets.Scripts.Models;
using UnityEngine;

[RequireComponent(typeof(ZoomAndPan))]
public class Cylinder : MonoBehaviour {

    private ZoomAndPan _zoomAndPan;
    private float _scaleX;
    private ModelMesh _currentMesh;
    private bool _enableZoomAndPan;

    public void SetItem(ModelMesh item, bool enableZoomAndPan)
    {
        _enableZoomAndPan = enableZoomAndPan;
        _zoomAndPan = GetComponent<ZoomAndPan>();
        _currentMesh = item;
        if (item.Scale.IsZero())
        {
            transform.localScale += new Vector3(_scaleX, 0, 0);
            _currentMesh.Scale = transform.localScale.Map();
            _currentMesh.Position = transform.position.Map();
        }
        transform.localScale = _currentMesh.Scale.Map();
        transform.position = _currentMesh.Position.Map();

    }

    void Update()
    {
        //TODO: Need to change to get initial touch!
        if (!_enableZoomAndPan) return;
        if (Input.touchCount == 1)
            _currentMesh.Position = _zoomAndPan.Pan().Map();
        if (Input.touchCount == 2)
        {
            var theScale = _zoomAndPan.Zoom();
            if (theScale != Vector3.zero) _currentMesh.Scale = theScale.Map();
        }
        //Debug.Log(string.Format("Zoom x: {0} z: {1}", _currentMesh.Scale.x, _currentMesh.Scale.z));
    }
}
