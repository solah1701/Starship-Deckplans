using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHelper : MonoBehaviour {

    public int LeftBounds = 150;
    public int RightBounds = 200;
    public int TopBounds = 40;
    public int BottomBounds = 0;

    private Touch _touch;

    private int _left, _right, _top, _bottom;

    void Start () {
        _left = LeftBounds;
        _right = Screen.width - RightBounds;
        _top = Screen.height - TopBounds;
        _bottom = BottomBounds;
    }

    public bool TouchInBounds()
    {
        if (Input.touchCount < 1) return false;
        _touch = Input.GetTouch(0);
        var pos = _touch.position;
        return !(pos.x < _left) && !(pos.x > _right) && !(pos.y > _top) && !(pos.y < _bottom);
    }

    public Touch GetTouch()
    {
        return _touch;
    }
}
