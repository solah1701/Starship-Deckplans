using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHelper : MonoBehaviour {

    public int LeftBounds = 150;
    public int RightBounds = 200;
    public int TopBounds = 40;
    public int BottomBounds = 0;
    public float ScaleXDen = 47;
    public float ScaleXNum = 635;
    public float ScaleXOff = 19;
    public float ScaleYDen = 50;
    public float ScaleYNum = 271;
    public float ScaleYOff = -6;

    private float ScaleX;
    private float ScaleY;

    private Touch _touch;

    private int _left, _right, _top, _bottom;

    void Start () {
        _left = LeftBounds;
        _right = Screen.width - RightBounds;
        _top = Screen.height - TopBounds;
        _bottom = BottomBounds;
    }

    public void CalculateScale()
    {
        ScaleX = CalculateScaleX();
        ScaleY = CalculateScaleY();
        //Debug.Log(string.Format("ScaleX {0} ScaleY {1}", ScaleX, ScaleY));
    }

    public float CalculatePosition(float cursor, float touch, float page)
    {
        return page - 2 * cursor * page / touch;
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

    public float GetScaleX()
    {
        return ScaleX;
    }

    public float GetScaleY()
    {
        return ScaleY ;
    }

    float CalculateScaleX()
    {
        return Screen.width * ScaleXDen / ScaleXNum + ScaleXOff;
    }

    float CalculateScaleY()
    {
        return Screen.height * ScaleYDen / ScaleYNum + ScaleYOff;
    }

}
