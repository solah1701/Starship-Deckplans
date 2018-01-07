using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Extenders;
using Assets.Scripts.Models;
using UnityEngine;

[RequireComponent(typeof(ZoomAndPan))]
public abstract class GameObjectBase : MonoBehaviour {

    private ZoomAndPan _zoomAndPan;
    private bool _enableZoomAndPan;

    protected abstract void UpdateZoom(Vect3 scale);
    protected abstract void UpdatePan(Vect3 position);

    protected void SetItem(bool enableZoomAndPan)
    {
        _enableZoomAndPan = enableZoomAndPan;
        _zoomAndPan = GetComponent<ZoomAndPan>();
    }

    void Update()
    {
        //TODO: Need to change to get initial touch!
        if (!_enableZoomAndPan) return;
        if (Input.touchCount == 1)
            UpdatePan(_zoomAndPan.Pan().Map());
        if (Input.touchCount == 2)
        {
            var theScale = _zoomAndPan.Zoom();
            if (theScale != Vector3.zero) UpdateZoom(theScale.Map());
        }
    }

}
