using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Extenders;
using UnityEngine;

public class ZoomAndPan : MonoBehaviour {

    private float _prevMagnitude = 0;

    public Vector3 Zoom()
    {
        var touch0 = Input.GetTouch(0);
        var touch1 = Input.GetTouch(1);
        var touch0Delta = touch0.deltaPosition;
        var touch1Delta = touch1.deltaPosition;
        var touch0Pos = touch0.position;
        var touch1Pos = touch1.position;
        var origVector = touch0Pos - touch0Delta - touch1Pos - touch1Delta;
        var diff = touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began ? 0 : origVector.magnitude - _prevMagnitude;
        _prevMagnitude = origVector.magnitude;
        if (touch0.phase != TouchPhase.Ended && touch1.phase != TouchPhase.Ended) return Zoom(diff);
        _prevMagnitude = 0;
        return new Vector3();
    }

    public Vector3 Zoom(float factor)
    {
        const float scale = 120;
        float n = 1.0f;
        if (transform.localScale.x > 3.0f)
            n = 3.0f;
        var zoomFactor = factor / (scale / n);
        var ratio = transform.localScale.x / transform.localScale.z;
        if (transform.localScale.x + zoomFactor < 0 || transform.localScale.z + zoomFactor < 0)
            return new Vector3();
        transform.localScale += new Vector3(zoomFactor * ratio, 0, zoomFactor);
        return transform.localScale;
    }

    public Vector3 Pan()
    {
        var myTouch = Input.GetTouch(0);
        Vector2 position = myTouch.deltaPosition;
        const float scale = 60;
        transform.Translate(-position.x / scale, 0, -position.y / scale);
        return transform.position;
    }
}
